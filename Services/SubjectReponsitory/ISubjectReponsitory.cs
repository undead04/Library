using Library.Data;
using Library.DTO;
using Library.Model;

namespace Library.Services.SubjectReponsitory
{
    public interface ISubjectReponsitory
    {
        Task CreateSubject(SubjectModel model);
        Task DeleteSubject(int Id);
        Task UpdateSubject(int Id, SubjectModel model);
        Task<List<SubjectDTO>> GetAll(string? search,int? subjectId, string? orderBy,string? UserId,StatusDocument? statusDocument);// a-z
        Task<SubjectDTO> GetById(int Id);
        
        Task<List<SubjectDTO>> GetAllSubjectClass(int ClassId);
    }
}
