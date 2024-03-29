
using Microsoft.AspNetCore.StaticFiles;
using System.IO;

namespace Library.Services.UploadService
{
    public class UploadService : IUploadService
    {
        private readonly IWebHostEnvironment environment;

        public UploadService(IWebHostEnvironment environment)
        {
            this.environment = environment;
            
        }

        public string GetFilePath(string ProCode,string name)
        {
            return environment.WebRootPath + $"\\Uploads\\{ProCode}\\{name}";
        }
        public async Task<string> UploadImage(string procode, IFormFile image)
        {
            
            string filePath = GetFilePath(procode,string.Empty);
            if (!System.IO.Directory.Exists(filePath))
            {
                System.IO.Directory.CreateDirectory(filePath);
            }
            string imagePath = filePath + $"\\{image.FileName}";
            using (FileStream fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return image.FileName;
        }
        public string GetUrlImage(string imageName,string proCode)
        {
            string hostUrl = "https://localhost:7019/";
            return hostUrl + $"Uploads/{proCode}/" + imageName;
        }
        public void DeleteImage(string ProCode, string nameImage)
        {
            string imagePath = GetFilePath(ProCode, nameImage);
           
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }

        public void RenameImage(string ProCode, string oldName,string newName)
        {
            string filePath = GetFilePath(ProCode, oldName);
            string filePathNew = GetFilePath(ProCode,newName);
            if(File.Exists(filePath))
            {
                File.Move(filePath,filePathNew);
            }

        }
        public string GetExtensionFile(string ProCode, string name)
        {
            string imagePath = GetFilePath(ProCode,name);
            return Path.GetExtension(imagePath);
        }
        public string GetSizeFile(string ProCode, string name)
        {
            string imagePath = GetFilePath(ProCode, name);
           
            FileInfo fileInfo = new FileInfo(imagePath);
            long fileSizeInBytes = fileInfo.Length;

            // Chuyển đổi kích thước từ byte sang các đơn vị khác như KB, MB, GB (tùy ý)
            double fileSizeInKB = (double)fileSizeInBytes / 1024; // Chia cho 1024 để chuyển đổi sang KB
            double fileSizeInMB = fileSizeInKB / 1024;
            return fileSizeInMB + " MB";
        }
        public async Task<(byte[], string, string)> DownloadFile(string FileName, string proCode)
        {
            try
            {
                var _GetFilePath = GetFilePath(proCode,FileName);
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(_GetFilePath, out var _ContentType))
                {
                    _ContentType = "application/octet-stream";
                }
                var _ReadAllBytesAsync = await File.ReadAllBytesAsync(_GetFilePath);
                return (_ReadAllBytesAsync, _ContentType, Path.GetFileName(_GetFilePath));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
