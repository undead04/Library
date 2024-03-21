using Library.Data;
using Library.DTO;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.MySubjectService
{
    public class MySubjectService : IMySubjectService
    {
        private readonly MyDB context;

        public MySubjectService(MyDB context) 
        {
            this.context = context;
        }
        public async Task<List<MySubjectStudentDTO>> SeenMySubjectStudent(int classId)
        {
            var subjects = await context.subjects
                .Include(f=>f.Major)!
                
                .Where(su=> su.subjectClassRooms!.Any(su => su.ClassRoomId == classId)).ToListAsync();
            return subjects.Select(su =>
            {
                var classSubject = context.subjectClassRooms.FirstOrDefault(su => su.ClassRoomId == classId && su.SubjectId == su.Id);
                return new MySubjectStudentDTO
                {
                    Id = su.Id,
                    Name = su.Name,
                    CodeSubejct = su.SubjectCode,
                    
                };
            }).ToList();

            

        }

        public Task<List<SubjectDTO>> SeenMySubjectTeacher(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
