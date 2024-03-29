using Library.Data;
using Library.Model;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.Office2010.Excel;
using Library.Model.DTO;

namespace Library.Repository.SystemNotificationRepository
{
    public class SystemNotificationRepository : ISystemNotificationRepository
    {
        private readonly MyDB context;

        public SystemNotificationRepository(MyDB context)
        {
            this.context = context;
        }

        public async Task CreateSystemNotification(SystemNotificationModel model)
        {
            var systemNotification = new SystemNotification
            {
                UserId = model.UserId,
                IsCancelExam = model.IsCancelExam,
                IsChangeLeListUser = model.IsChangeLeListUser,
                IsChangePassword = model.IsChangePassword,
                IsChangleListRole = model.IsChangleListRole,
                IsCommentMyQuestion = model.IsCommentMyQuestion,
                IsCommentNotification = model.IsCommentNotification,
                IsContentSubject = model.IsContentSubject,
                IsCreateNotificationSubject = model.IsCreateNotificationSubject,
                IscrudExam = model.IscrudExam,
                IscrudLesson = model.IscrudLesson,
                IsCrudPrivateFile = model.IsCrudPrivateFile,
                IscrudQuestion = model.IscrudQuestion,
                IscrudResource = model.IscrudResource,
                IsListSUbject = model.IsListSUbject,
                IsSaveExam = model.IsSaveExam,
                IsTeacherQuestionSubject = model.IsTeacherQuestionSubject,
                IsUpdateInformationUser = model.IsUpdateInformationUser,
                IscrudDocument=model.iscrudDocument
            };
            await context.AddAsync(systemNotification);
            await context.SaveChangesAsync();
        }

        public async Task DeleteSystemNotification(string userId)
        {
            var systemNotification = await context.systemNotifications.FirstOrDefaultAsync(no => no.UserId == userId);
            if (systemNotification != null)
            {
                context.Remove(systemNotification);
                await context.SaveChangesAsync();
            }
        }

        public async Task<SystemNotificationUserDTO> GetSystemNotification(string userId)
        {
            var systemNotification = await context.systemNotifications.FirstOrDefaultAsync(no => no.UserId == userId);
            if (systemNotification != null)
            {
                return new SystemNotificationUserDTO
                {
                    Id = systemNotification.Id,
                    IsCancelExam = systemNotification.IsCancelExam,
                    IsChangeLeListUser = systemNotification.IsChangeLeListUser,
                    IsChangePassword = systemNotification.IsChangePassword,
                    UserId = systemNotification.UserId,
                    IsChangleListRole = systemNotification.IsChangleListRole,
                    IsCommentMyQuestion = systemNotification.IsCommentMyQuestion,
                    IsCommentNotification = systemNotification.IsCommentNotification,
                    IsContentSubject = systemNotification.IsContentSubject,
                    IsCreateNotificationSubject = systemNotification.IsCreateNotificationSubject,
                    IscrudDocument = systemNotification.IscrudDocument,
                    IscrudExam = systemNotification.IscrudExam,
                    IscrudLesson = systemNotification.IscrudLesson,
                    IsCrudPrivateFile = systemNotification.IsCrudPrivateFile,
                    IscrudQuestion = systemNotification.IscrudQuestion,
                    IscrudResource = systemNotification.IscrudResource,
                    IsListSUbject = systemNotification.IsListSUbject,
                    IsSaveExam = systemNotification.IsSaveExam,
                    IsTeacherQuestionSubject = systemNotification.IsTeacherQuestionSubject,
                    IsUpdateInformationUser = systemNotification.IsUpdateInformationUser,

                };
            }
            return null;
        }

        public async Task UpdateSystemNotification(int id, SystemNotificationModel model)
        {
            var systemNotification = await context.systemNotifications.FirstOrDefaultAsync(no => no.Id == id);
            if (systemNotification != null)
            {
                systemNotification.IsCancelExam = model.IsCancelExam;
                systemNotification.IsChangeLeListUser = model.IsChangeLeListUser;
                systemNotification.IsChangePassword = model.IsChangePassword;
                systemNotification.IsChangleListRole = model.IsChangleListRole;
                systemNotification.IsCommentMyQuestion = model.IsCommentMyQuestion;
                systemNotification.IsCommentNotification = model.IsCommentNotification;
                systemNotification.IsContentSubject = model.IsContentSubject;
                systemNotification.IsCreateNotificationSubject = model.IsCreateNotificationSubject;
                systemNotification.IscrudExam = model.IscrudExam;
                systemNotification.IscrudLesson = model.IscrudLesson;
                systemNotification.IsCrudPrivateFile = model.IsCrudPrivateFile;
                systemNotification.IscrudQuestion = model.IscrudQuestion;
                systemNotification.IscrudResource = model.IscrudResource;
                systemNotification.IsListSUbject = model.IsListSUbject;
                systemNotification.IsSaveExam = model.IsSaveExam;
                systemNotification.IsTeacherQuestionSubject = model.IsTeacherQuestionSubject;
                systemNotification.IsUpdateInformationUser = model.IsUpdateInformationUser;
                systemNotification.IscrudDocument = model.iscrudDocument;
                await context.SaveChangesAsync();
            }
        }
    }
}
