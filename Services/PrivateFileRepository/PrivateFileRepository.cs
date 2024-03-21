using Library.Data;
using Library.DTO;
using Library.Model;
using Library.Services.UploadService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection.Metadata;

namespace Library.Services.PrivateFileRepository
{
    public class PrivateFileRepository : IPrivateFileRepository
    {
        private readonly MyDB context;
        private readonly IUploadService uploadService;

        public PrivateFileRepository(MyDB context,IUploadService uploadService)
        {
            this.context = context;
            this.uploadService=uploadService;
        }
        public async Task DeletePrivateFile(int id)
        {
            var privateFile = await context.privateFiles.FirstOrDefaultAsync(doc => doc.Id == id);
            if (privateFile != null)
            {
                uploadService.DeleteImage("PrivateFile", privateFile.Name);
                context.Remove(privateFile);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<PrivateFileDTO>> GetAll()
        {
            var privateFile = await context.privateFiles.Include(f=>f.User).ToListAsync();
            return privateFile.Select(pr => new PrivateFileDTO
            {
                Id = pr.Id,
                Name = pr.Name,
                Size = pr.Size,
                Type = pr.Type,
                UrlFile=uploadService.GetUrlImage(pr.Name, "PrivateFile"),
                Create_at=pr.Create_at,
                CreateUserName=pr.User!.UserName,
            }).ToList();
        }

        public async Task<PrivateFileDTO> GetById(int id)
        {
            var privateFile = await context.privateFiles.Include(f => f.User).FirstOrDefaultAsync(pr=>pr.Id==id);
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

        public async Task RenamePrivateFile(int id,string name)
        {
            var privateFile = await context.privateFiles.FirstOrDefaultAsync(doc => doc.Id == id);
            if (privateFile != null)
            {
                string newName = $"{name}{privateFile.Type}";
                uploadService.RenameImage("PrivateFile", privateFile.Name, newName);
                privateFile.Name = newName;
                privateFile.Create_at = DateTime.Now.Date.ToString();
                await context.SaveChangesAsync();

            }
        }

        public async Task UploadPrivateFile(PrivateFileModel model)
        {
            foreach(var file in model.Files!) 
            {
                var privateFile = new PrivateFile
                {
                    Create_at=DateTime.Now.Date.ToString(),
                    CreateUserId=model.CreateUserId
                };
                await context.privateFiles.AddAsync(privateFile);
                await context.SaveChangesAsync();
                var nameFile =await uploadService.UploadImage(privateFile.Id, "PrivateFile", file);
                privateFile.Name = nameFile;
                privateFile.Type = uploadService.GetExtensionFile("PrivateFile", file);
                privateFile.Size = uploadService.GetSizeFile("PrivateFile", file);
                await context.SaveChangesAsync();
            }
        }
    }
}
