using Library.DTO;
using Library.Model;

namespace Library.Server
{
    public interface IClassReponsitory
    {
        Task CreateClass (ClassRoomModel model);
        Task UpdateClass (int Id,ClassRoomModel model);
        Task DeleteClass (int Id);
        Task<List<ClassRoomDTO>> GetAllClass();
        Task<ClassRoomDTO> GetById (int Id);
        
        Task AddSubjectClass(int ClassId,int SubjectId,int teacherId);

    }
}
