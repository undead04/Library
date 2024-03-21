using Library.Data;
using Library.Model;
using Library.Services.UploadService;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace Library.Services.ExcelService
{
    public class ExcelService : IExcelService
    {
        private readonly IUploadService uploadService;
        private readonly MyDB context;

        public ExcelService(IUploadService uploadService,MyDB context) 
        { 
            this.uploadService=uploadService;
            this.context = context;
        
        }
        public async Task CreateExcel(string name, List<int> Questionids)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Hoặc Commercial, tùy thuộc vào loại giấy phép bạn sử dụng
            var filthPath = uploadService.GetFilePath("Exam") + $"\\{name}.xlsx";
            var fileInfo = new FileInfo(filthPath);
            using (var package = new ExcelPackage())
            {
                // Tạo một Sheet mới và lấy nó từ tệp Excel
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells[1, 1].Value = "Cấp độ";
                worksheet.Cells[1, 2].Value = "Câu hỏi";
                worksheet.Cells[1, 3].Value = "người tạo câu hỏi";
                worksheet.Cells[1, 4].Value = "Ngày tạo";
                worksheet.Cells[1, 5].Value = "Đáp án A";
                worksheet.Cells[1, 5].Value = "Đáp án B";
                worksheet.Cells[1, 7].Value = "Đáp án C";
                worksheet.Cells[1, 8].Value = "Đáp án D";
                worksheet.Cells[1, 9].Value = "Đâp án đúng";

                // Dòng bắt đầu
                int startRow = 2;

                // Duyệt qua các questionId từ danh sách Questionids
                foreach (int questionId in Questionids)
                {
                    var question = await context.questions.Where(qu => qu.Id == questionId).FirstOrDefaultAsync();
                    if (question != null)
                    {
                        worksheet.Cells[startRow, 1].Value = question.Title;
                        worksheet.Cells[startRow, 2].Value = question.Level;
                        worksheet.Cells[startRow, 3].Value = question.Context;
                        worksheet.Cells[startRow, 4].Value = question.CreateUserId;
                        worksheet.Cells[startRow, 5].Value = question.Create_at;
                        int correctAnswer = 0;
                        for (int i = 0; i <= 3; i++)
                        {
                            worksheet.Cells[startRow, 5 + i].Value = question.Answers!.ElementAt(i).context;
                            if (question.Answers!.ElementAt(i).Istrue)
                            {
                                correctAnswer = i;
                            }
                        }
                        worksheet.Cells[startRow, 10].Value = Convert.ToChar(correctAnswer + 65);

                        startRow++;
                    }
                }

                // Lưu tệp Excel
                package.SaveAs(fileInfo);
            }

        }
    }
}
