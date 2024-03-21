﻿using Library.Services.DocumentRepository;
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

        public string GetFilePath(string ProCode)
        {
            return environment.WebRootPath + $"\\Uploads\\{ProCode}";
        }
        public async Task<string> UploadImage(int id, string procode, IFormFile image)
        {
            
            var filePath = GetFilePath(procode);
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
            var filePath = GetFilePath(ProCode);
            var imagePath = filePath + $"\\{nameImage}";
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }

        public void RenameImage(string ProCode, string oldName,string newName)
        {
            var filePath = GetFilePath(ProCode) + $"\\{oldName}";
            var filePathNew = GetFilePath(ProCode) + $"\\{newName}";
            if(File.Exists(filePath))
            {
                File.Move(filePath,filePathNew);
            }

        }
        public string GetExtensionFile(string ProCode, IFormFile image)
        {
            var filePath = GetFilePath(ProCode);
            string imagePath = filePath + $"\\{image.FileName}";
            return Path.GetExtension(imagePath);
        }
        public string GetSizeFile(string ProCode, IFormFile image)
        {
            var filePath = GetFilePath(ProCode);
            string imagePath = filePath + $"\\{image.FileName}";
            FileInfo fileInfo = new FileInfo(imagePath);
            long fileSizeInBytes = fileInfo.Length;

            // Chuyển đổi kích thước từ byte sang các đơn vị khác như KB, MB, GB (tùy ý)
            double fileSizeInKB = (double)fileSizeInBytes / 1024; // Chia cho 1024 để chuyển đổi sang KB
            double fileSizeInMB = fileSizeInKB / 1024;
            return fileSizeInMB + " MB";
        }
    }
}
