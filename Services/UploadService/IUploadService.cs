namespace Library.Services.UploadService
{
    public interface IUploadService
    {
        Task<string> UploadImage(string procode, IFormFile image);
        string GetFilePath(string ProCode,string name);
        string GetUrlImage(string imageName,string proCode);
        void DeleteImage(string ProCode, string nameImage);
        void RenameImage(string ProCode, string oldName,string newName);
        string GetExtensionFile(string ProCode,string name);
        string GetSizeFile(string ProCode,string name);
        Task<(byte[], string, string)> DownloadFile(string FileName,string Procode);

    }
}
