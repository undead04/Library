using Library.Model;

namespace Library.Services.ExcelService
{
    public interface IExcelService
    {
        Task CreateExcel(string name, List<int> questionids);
    }
}
