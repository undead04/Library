namespace Library.Services.UploadService
{
    public interface IUploadService
    {
        Task<string> UploadImage(int id, string procode, IFormFile image);
        string GetFilePath(string ProCode);
        string GetUrlImage(string imageName,string proCode);
        void DeleteImage(string ProCode, string nameImage);
        void RenameImage(string ProCode, string oldName,string newName);
        string GetExtensionFile(string ProCode,IFormFile image);
        string GetSizeFile(string ProCode,IFormFile image);
        
    }
}
