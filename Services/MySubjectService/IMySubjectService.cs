using Library.DTO;

namespace Library.Services.MySubjectService
{
    public interface IMySubjectService
    {
        Task<List<SubjectDTO>> SeenMySubjectTeacher(string userId);
        Task<List<MySubjectStudentDTO>> SeenMySubjectStudent(int classId);
    }
}
