using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Library.Data;
using Library.Model;
using Library.Services.MultipleChoiceRepository;
using Library.Services.UploadService;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Document = DocumentFormat.OpenXml.Wordprocessing.Document;
using Run = DocumentFormat.OpenXml.Spreadsheet.Run;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace Library.Services.ExcelService
{
    public class ExcelService : IExcelService
    {
        private readonly IUploadService uploadService;
        private readonly MyDB context;
        private readonly IMultipleChoiceRepository multipleChoiceRepository;

        public ExcelService(IUploadService uploadService,MyDB context,IMultipleChoiceRepository multipleChoiceRepository) 
        { 
            this.uploadService=uploadService;
            this.context = context;
            this.multipleChoiceRepository = multipleChoiceRepository;
        }
        public async Task CreateExcel(string name, List<int> Questionids)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Hoặc Commercial, tùy thuộc vào loại giấy phép bạn sử dụng
            var filthPath = uploadService.GetFilePath("Exam", name);
            var fileInfo = new FileInfo(filthPath);
            using (var package = new ExcelPackage())
            {
                // Tạo một Sheet mới và lấy nó từ tệp Excel
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells[1, 1].Value = "Cấp độ";
                worksheet.Cells[1, 2].Value = "Câu hỏi";
                worksheet.Cells[1, 3].Value = "Đáp án A";
                worksheet.Cells[1, 4].Value = "Đáp án B";
                worksheet.Cells[1, 5].Value = "Đáp án C";
                worksheet.Cells[1, 6].Value = "Đáp án D";
                worksheet.Cells[1, 7].Value = "Đâp án đúng";

                // Dòng bắt đầu
                int startRow = 2;

                // Duyệt qua các questionId từ danh sách Questionids
                foreach (int questionId in Questionids)
                {
                    var question = await context.questions.Where(qu => qu.Id == questionId).FirstOrDefaultAsync();
                    if (question != null)
                    {
                       
                        worksheet.Cells[startRow, 1].Value = question.Level;
                        worksheet.Cells[startRow, 2].Value = question.Context;
                        int correctAnswer = 0;
                        for (int i = 0; i <= 3; i++)
                        {
                            worksheet.Cells[startRow, 3 + i].Value = question.Answers!.ElementAt(i).context;
                            if (question.Answers!.ElementAt(i).Istrue)
                            {
                                correctAnswer = i;
                            }
                        }
                        worksheet.Cells[startRow, 7].Value = Convert.ToChar(correctAnswer + 65);

                        startRow++;
                    }
                }

                // Lưu tệp Excel
                package.SaveAs(fileInfo);
            }

        }

        public async Task CreateWord(string name, List<int> questionId)
        {
            var filthPath = uploadService.GetFilePath("Exam",name);
           

            // Tạo một tệp Word mới
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(filthPath, WordprocessingDocumentType.Document))
            {
                // Tạo một phần văn bản
                MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());
                int i = 1;
                // Thêm nội dung vào tệp Word
                foreach(int id in questionId)
                {
                    var examEssay = await context.essayExams.FirstOrDefaultAsync(es => es.Id == id);
                   if(examEssay!=null)
                    {
                        string question = examEssay.Context;
                        Paragraph para = body.AppendChild(new Paragraph());
                        Run run = para.AppendChild(new Run());
                        run.AppendChild(new Text($"Câu {i}: {question}"));
                        i++;

                    }
                }
                wordDoc.Save();
            }
        }

        public async Task<List<int>> GetExcel(int subjectId,string UserId,string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Hoặc Commercial, tùy thuộc vào loại giấy phép bạn sử dụng
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Lấy worksheet đầu tiên
                int startRow = 2;
                List<int> listQuestionId = new List<int>();
                // Duyệt qua các dòng trong worksheet
                for (int row = startRow; row <= worksheet.Dimension.End.Row; row++)
                {
                    List<AnswerModel> listAnswers = new List<AnswerModel>();
                    for(int i=0;i<=3;i++)
                    {
                        var answer = new AnswerModel
                        {
                            context = worksheet.Cells[row, 5+i].Value?.ToString(),
                            CorrectAnswer = 65+i == Convert.ToInt32(Convert.ToChar(worksheet.Cells[row, 9].Value)),
                        };
                        listAnswers.Add(answer);
                    }
                    
                    var questionModel = new QuestionModel
                    {
                       
                        Context = worksheet.Cells[row, 2].Value?.ToString(),
                        Level = worksheet.Cells[row, 1].Value?.ToString(),
                        SubjectId =subjectId,
                        answers=listAnswers.ToArray(),

                    };
                    int questionid =await multipleChoiceRepository.CreateQuestion(questionModel);
                    listQuestionId.Add(questionid);
                }
                return listQuestionId;
            }
        }

        public async Task GetWord(int id,string filePath)
        {
            var exam = await context.exams.FirstOrDefaultAsync(ex => ex.Id == id);
            if(exam!=null)
            {
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filePath, false))
                {
                    string content = string.Empty;

                    // Đọc nội dung từ tệp Word
                    foreach (var paragraph in wordDoc.MainDocumentPart.Document.Body.Elements<Paragraph>())
                    {

                        string contentQuestions = paragraph.InnerText.Trim();
                        int index = contentQuestions.IndexOf(":");
                        contentQuestions = contentQuestions.Substring(index+1).Trim();
                        var quesionExam = new EssayExam
                        {
                            Context = contentQuestions,
                            ExamId = exam.Id,

                        };
                        await context.essayExams.AddAsync(quesionExam);
                        await context.SaveChangesAsync();

                    }
                }
            }
        }
    }
}
