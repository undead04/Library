using Library.Data;
using Library.DTO;
using Library.Model;
using Microsoft.EntityFrameworkCore;

namespace Library.Server
{
    public class LessonReponsitory : ILessonReponsitory
    {
        private readonly MyDB context;

        public LessonReponsitory(MyDB context) 
        { 
            this.context=context;
        }
        public async Task CreateLesson(LessonModel model)
        {
            var document = new Document
            {
                Classify = TypeDocument.Lesson,
                Name = model.File!.FileName,
                Create_at = DateTime.Now,
                SubjectId=model.SubjectId,
            };
            await context.documents.AddAsync(document);
            await context.SaveChangesAsync();
            var lesson = new Lesson
            {
                Title = model.Title,
                TopicId = model.TopicId,
                DoucumentId=document.Id,
            };
            await context.lessons.AddAsync(lesson);
            await context.SaveChangesAsync();
            var filePath=Path.Combine(Directory.GetCurrentDirectory(),"Upload",model.File!.FileName);
            using(var streamFile=new FileStream(filePath,FileMode.Create))
            { 
                model.File.CopyTo(streamFile);
            }
        }

        public async Task DeleteLesson(int Id)
        {
            var lesson = await context.lessons.FirstOrDefaultAsync(le => le.Id == Id);
            if(lesson!=null)
            {
                context.lessons.Remove(lesson);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<LessonDTO>> GetAllLesson(int TopicId)
        {
            return await context.lessons.Where(le=>le.TopicId==TopicId).Select(le => new LessonDTO
            {
                Id = le.Id,
                Title = le.Title,
                DocumentId=le.DoucumentId,
            }).ToListAsync();
        }

        public async Task<LessonDTO> GetLessonById(int Id)
        {
           var lesson=await context.lessons.FirstOrDefaultAsync(le=>le.Id == Id);
            if(lesson!=null)
            {
                return new LessonDTO
                {
                    Id = lesson.Id,
                    Title = lesson.Title,
                    DocumentId = lesson.DoucumentId,
                };
            }
            return null;
        }

        public async Task UpdateLesson(int Id, LessonModel model)
        {
            var lesson = await context.lessons.FirstOrDefaultAsync(le => le.Id == Id);
            if (lesson != null)
            {
                lesson.Title = model.Title;
                
                await context.SaveChangesAsync();
            }
        }
    }
}
