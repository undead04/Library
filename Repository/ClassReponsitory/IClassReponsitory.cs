using Library.Model;
using Library.Model.DTO;

namespace Library.Repository.ClassReponsitory
{
    public interface IClassReponsitory
    {
        Task CreateClass(ClassRoomModel model);
        Task UpdateClass(int Id, ClassRoomModel model);
        Task DeleteClass(int Id);
        Task<List<ClassRoomDTO>> GetAllClass();
        Task<ClassRoomDTO> GetById(int Id);
        Task<List<ClassRoomDTO>> GetAllCLassRoomSubject(int subjectId);
        Task AddSubjectClass(AddTearcherClassRoom model);

    }
}
