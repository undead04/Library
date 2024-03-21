using Library.Data;
using Library.Model;

namespace Library.Services.ClassLessonRepository
{
    public class ClassLessonRepository : IClassLessonRepository
    {
        private readonly MyDB context;

        public ClassLessonRepository(MyDB context) 
        { 
            this.context=context;
            
        }
        public async Task AssignDocuments(AssignDocumentModel model)
        {
           foreach(int classId in model.ClassId)
            {
                foreach(int lessonId in model.LessonId)
                {
                    var classLesson = new ClassLesson
                    {
                        ClassId = classId,
                        LessonId=lessonId,
                    };
                    await context.classLessons.AddAsync(classLesson);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
