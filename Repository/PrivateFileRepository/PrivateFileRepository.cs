using DocumentFormat.OpenXml.Spreadsheet;
using Library.Data;
using Library.Model;
using Library.Model.DTO;
using Library.Repository.SystemNotificationRepository;
using Library.Services.JWTService;
using Library.Services.NotificationService;
using Library.Services.UploadService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection.Metadata;

namespace Library.Repository.PrivateFileRepository
{
    public class PrivateFileRepository : IPrivateFileRepository
    {
        private readonly MyDB context;
        private readonly IUploadService uploadService;
        private readonly IJWTSevice jWTSevice;
        private readonly INotificationService notificationService;

        public PrivateFileRepository(MyDB context, IUploadService uploadService, IJWTSevice jWTSevice,
            INotificationService notificationService)
        {
            this.context = context;
            this.uploadService = uploadService;
            this.jWTSevice = jWTSevice;
            this.notificationService = notificationService;
        }
        public async Task DeletePrivateFile(int id)
        {
            var userId = await jWTSevice.ReadToken();
            var privateFile = await context.privateFiles.FirstOrDefaultAsync(doc => doc.Id == id);
            if (privateFile != null)
            {
                uploadService.DeleteImage("PrivateFile", privateFile.Name);
                context.Remove(privateFile);
                List<string> listUserId = new List<string>() { userId };
                await notificationService.CreateNotification(TypeNotification.IsCrudPrivateFile,$"Bạn đã xóa tệp riêng tư {privateFile.Name} thành công", listUserId, userId);


                await context.SaveChangesAsync();
            }
        }

        public async Task<List<PrivateFileDTO>> GetAll()
        {
            var privateFile = await context.privateFiles.Include(f => f.User).ToListAsync();
            return privateFile.Select(pr => new PrivateFileDTO
            {
                Id = pr.Id,
                Name = pr.Name,
                Size = pr.Size,
                Type = pr.Type,
                UrlFile = uploadService.GetUrlImage(pr.Name, "PrivateFile"),
                Create_at = pr.Create_at,
                CreateUserName = pr.User!.UserName,
            }).ToList();
        }

        public async Task<PrivateFileDTO> GetById(int id)
        {
            var privateFile = await context.privateFiles.Include(f => f.User).FirstOrDefaultAsync(pr => pr.Id == id);
            if (privateFile == null)
            {
                return null;
            }
            return new PrivateFileDTO
            {
                Id = privateFile.Id,
                Name = privateFile.Name,
                Size = privateFile.Size,
                Type = privateFile.Type,
                UrlFile = uploadService.GetUrlImage(privateFile.Name, "PrivateFile"),
                Create_at = privateFile.Create_at,
                CreateUserName = privateFile.User!.UserName,
            };
        }

        public async Task RenamePrivateFile(int id, string name)
        {
            var userId = await jWTSevice.ReadToken();
            var privateFile = await context.privateFiles.FirstOrDefaultAsync(doc => doc.Id == id);
            if (privateFile != null)
            {
                string newName = $"{name}{privateFile.Type}";
                uploadService.RenameImage("PrivateFile", privateFile.Name, newName);
                privateFile.Name = newName;
                privateFile.Create_at = DateTime.Now.Date.ToString();
                List<string> listUserId = new List<string>() { userId };
                await notificationService.CreateNotification(TypeNotification.IsCrudPrivateFile,$"Bạn đã đổi tên tệp riêng tư từ {privateFile.Name} thành {newName}", listUserId, userId);
                await context.SaveChangesAsync();
            }
        }

        public async Task UploadPrivateFile(PrivateFileModel model)
        {
            var userId = await jWTSevice.ReadToken();
            foreach (var file in model.Files!)
            {
                var privateFile = new PrivateFile
                {
                    Create_at = DateTime.Now.Date.ToString(),
                    CreateUserId = userId,
                };
                await context.privateFiles.AddAsync(privateFile);
                await context.SaveChangesAsync();
                var nameFile = await uploadService.UploadImage("PrivateFile", file);
                privateFile.Name = nameFile;
                privateFile.Type = uploadService.GetExtensionFile("PrivateFile", nameFile);
                privateFile.Size = uploadService.GetSizeFile("PrivateFile", nameFile);
                List<string> listUserId = new List<string>() { userId };
                await notificationService.CreateNotification(TypeNotification.IscrudQuestion, $"Bạn tải tệp riêng tư tên {privateFile.Name} lên", listUserId, userId);
                await context.SaveChangesAsync();
            }
        }
    }
}
