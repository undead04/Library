using DocumentFormat.OpenXml.Spreadsheet;
using Library.Data;
using Library.Model;
using Library.Model.DTO;
using Library.Repository.SystemNotificationRepository;
using Library.Services.JWTService;
using Library.Services.NotificationService;
using Library.Services.ReissuePassword;
using Library.Services.UploadService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace Library.Repository.UserReponsitory
{
    public class UserReponsitory : IUserReponsitory
    {
        private readonly MyDB context;
        private UserManager<ApplicationUser> userManager;

        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IReissuePassword reissuePassword;
        private readonly ISystemNotificationRepository systemNotificationRepository;
        private readonly IUploadService uploadService;

        public UserReponsitory(MyDB context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<Role> roleManager,
            IReissuePassword reissuePassword, ISystemNotificationRepository systemNotificationRepository, IUploadService uploadService)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.reissuePassword = reissuePassword;
            this.systemNotificationRepository = systemNotificationRepository;
            this.uploadService = uploadService;

        }



        public async Task ChanglePassWord(ChanglePassWordModel model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            bool isEqual = model.comformPassword == model.newPassWord;
            if (isEqual)
            {
                await userManager.ChangePasswordAsync(user, model.presentPassWord, model.newPassWord);
                await signInManager.RefreshSignInAsync(user);
            }

        }
        public async Task UpdateImage(IFormFile Avatar, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var nameImage = await uploadService.UploadImage("Avatar", Avatar);
            user.Avatar = nameImage;
            await userManager.UpdateAsync(user);
        }
        public async Task DeleteImage(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user.Avatar != null)
            {
                uploadService.DeleteImage("Avatar", user.Avatar);
                user.Avatar = string.Empty;
                await userManager.UpdateAsync(user);
            }
        }
        public async Task<List<UserDTO>> GetAllUser(string? search, string? roleId)
        {
            var users = await userManager.Users.ToListAsync();
            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(us => us.UserCode.Contains(search) || us.UserName.Contains(search) || us.Email.Contains(search)).ToList();
            }
            if (!string.IsNullOrEmpty(roleId))
            {
                var role = await roleManager.FindByIdAsync(roleId);
                if (role != null)
                {
                    var userRole = await userManager.GetUsersInRoleAsync(role.Name!);
                    users = userRole.ToList();
                }
            }
            return users.Select(user => new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                CodeUser = user.UserCode,
                Sex = user.Sex,
                Phone = user.PhoneNumber,
                Address = user.Address,
                Avatar = user.Avatar,
                UrlAvatar = uploadService.GetUrlImage(user.Avatar, "Avatar"),

            }).ToList();
        }
        public async Task DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                var nameListRole = await userManager.GetRolesAsync(user);
                string nameRole = string.Join("", nameListRole);
                if (nameRole == AppRole.Teacher)
                {
                    var teacher = await context.tearchers.FirstOrDefaultAsync(te => te.UserId == user.Id);
                    if (teacher != null)
                    {
                        context.tearchers.Remove(teacher);
                        await context.SaveChangesAsync();
                    }
                }
                if (nameRole == AppRole.Student)
                {
                    var student = await context.students.FirstOrDefaultAsync(te => te.UserId == user.Id);
                    if (student != null)
                    {
                        context.students.Remove(student);
                        await context.SaveChangesAsync();
                    }
                }
                await systemNotificationRepository.DeleteSystemNotification(id);
                await userManager.DeleteAsync(user);

            }
        }
        public async Task<UserDTO> GetUserById(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                CodeUser = user.UserCode,
                Sex = user.Sex,
                Phone = user.PhoneNumber,
                Address = user.Address,
                Avatar = uploadService.GetUrlImage(user.Avatar, "Avatar"),
                Major = user.Tearcher != null ? user.Tearcher.major!.Name : string.Empty,
                Class = user.Student != null ? user.Student.classRoom!.Name : string.Empty,

            };
        }
        public async Task CreateUser(SingnUpModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                Sex = model.Sex,
                UserCode = model.UserCode,
                Address = model.Address,
                PhoneNumber = model.Phone,
                Avatar = string.Empty

            };
            string password = reissuePassword.CreatPassword();
            var result = await userManager.CreateAsync(user, password);
            var role = await roleManager.FindByIdAsync(model.RoleId);
            if (result.Succeeded)
            {
                if (await roleManager.RoleExistsAsync(role.Name))
                {
                    await userManager.AddToRoleAsync(user, role.Name);
                }
                switch (role.Name)
                {
                    case AppRole.Student:
                        var student = new Student
                        {
                            UserId = user.Id,
                            ClassRoomId = model.ClassRoomId,
                            MajorId = model.MajorsId,
                        };
                        var systemNotification = new SystemNotificationModel
                        {
                            IsChangePassword = true,
                            IsUpdateInformationUser = true,
                            IsCommentNotification = true,
                            IsCreateNotificationSubject = true,
                            IsCommentMyQuestion = true,
                            IsTeacherQuestionSubject = true,
                            UserId = user.Id,
                        };
                        await systemNotificationRepository.CreateSystemNotification(systemNotification);
                        await context.students.AddAsync(student);
                        await context.SaveChangesAsync();
                        break;
                    case AppRole.Teacher:
                        var teacher = new Tearcher
                        {
                            UserId = user.Id,
                            MajorId = model.MajorsId,
                        };
                        await context.tearchers.AddAsync(teacher);
                        await context.SaveChangesAsync();
                        var systemNotificationTeacher = new SystemNotificationModel
                        {
                            IsChangePassword = true,
                            IsUpdateInformationUser = true,
                            UserId = user.Id,
                            IscrudExam = true,
                            IscrudQuestion = true,
                            IscrudLesson = true,
                            IscrudResource = true,
                            IsCommentNotification = true,
                            IsCommentMyQuestion = true,
                            iscrudDocument = true,
                        };
                        await systemNotificationRepository.CreateSystemNotification(systemNotificationTeacher);
                        break;
                    case AppRole.Leader:
                        var systemNotificationLeader = new SystemNotificationModel
                        {
                            IsChangePassword = true,
                            IsUpdateInformationUser = true,
                            UserId = user.Id,
                            IsChangleListRole = true,
                            IsChangeLeListUser = true,
                            IsCancelExam = true,
                            IsSaveExam = true,
                            IsCrudPrivateFile = true,
                            IsContentSubject = true,
                            IsListSUbject = true,
                        };
                        await systemNotificationRepository.CreateSystemNotification(systemNotificationLeader);
                        break;

                }

            }



        }
        public async Task UpdateUser(string Id, SingnUpModel model)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user != null)
            {

                user.UserName = model.UserName;
                user.Email = model.Email;
                user.Sex = model.Sex;
                user.UserCode = model.UserCode;
                user.Address = model.Address;
                user.PhoneNumber = model.Phone;
                var role = await roleManager.FindByIdAsync(model.RoleId);
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var rolePresnt = await userManager.GetRolesAsync(user);
                    string nameRole = string.Join("", rolePresnt);
                    if (!await roleManager.RoleExistsAsync(role.Name) && nameRole != role.Name)
                    {
                        await userManager.RemoveFromRoleAsync(user, nameRole);
                        await userManager.AddToRoleAsync(user, role.Name);
                    }
                    if (nameRole == AppRole.Student)
                    {
                        var student = await context.students.FirstOrDefaultAsync(x => x.UserId == user.Id);
                        if (student != null)
                        {
                            context.students.Remove(student);
                            await context.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        var teacher = await context.tearchers.FirstOrDefaultAsync(x => x.UserId == user.Id);
                        if (teacher != null)
                        {
                            context.tearchers.Remove(teacher);
                            await context.SaveChangesAsync();
                        }
                    }
                    switch (role.Name)
                    {
                        case AppRole.Student:
                            var student = new Student
                            {
                                UserId = user.Id,
                                ClassRoomId = model.ClassRoomId,
                                MajorId = model.MajorsId,
                            };
                            var systemNotification = new SystemNotificationModel
                            {
                                IsChangePassword = true,
                                IsUpdateInformationUser = true,
                                IsCommentNotification = true,
                                IsCreateNotificationSubject = true,
                                IsCommentMyQuestion = true,
                                IsTeacherQuestionSubject = true,
                                UserId = user.Id,
                            };
                            await systemNotificationRepository.CreateSystemNotification(systemNotification);
                            await context.students.AddAsync(student);
                            await context.SaveChangesAsync();
                            break;
                        case AppRole.Teacher:
                            var teacher = new Tearcher
                            {
                                UserId = user.Id,
                                MajorId = model.MajorsId,
                            };
                            await context.tearchers.AddAsync(teacher);
                            await context.SaveChangesAsync();
                            var systemNotificationTeacher = new SystemNotificationModel
                            {
                                IsChangePassword = true,
                                IsUpdateInformationUser = true,
                                UserId = user.Id,
                                IscrudExam = true,
                                IscrudQuestion = true,
                                IscrudLesson = true,
                                IscrudResource = true,
                                IsCommentNotification = true,
                                IsCommentMyQuestion = true,
                                iscrudDocument = true,
                            };
                            await systemNotificationRepository.CreateSystemNotification(systemNotificationTeacher);
                            break;
                        case AppRole.Leader:
                            var systemNotificationLeader = new SystemNotificationModel
                            {
                                IsChangePassword = true,
                                IsUpdateInformationUser = true,
                                UserId = user.Id,
                                IsChangleListRole = true,
                                IsChangeLeListUser = true,
                                IsCancelExam = true,
                                IsSaveExam = true,
                                IsCrudPrivateFile = true,
                                IsContentSubject = true,
                                IsListSUbject = true,
                            };
                            await systemNotificationRepository.CreateSystemNotification(systemNotificationLeader);
                            break;
                    }

                }


            }


        }


    }
}
