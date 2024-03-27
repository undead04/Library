using Library.Model;

namespace Library.Services.ExcelService
{
    public interface IExcelService
    {
        Task CreateExcel(string name, List<int> questionids);
        Task CreateWord(string name, List<int> question);
        Task<List<int>> GetExcel(int subjectId,string UserId,string filePath);
        Task GetWord(int id,string filePath);
    }
}
