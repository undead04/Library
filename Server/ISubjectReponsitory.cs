using Library.DTO;
using Library.Model;

namespace Library.Server
{
    public interface ISubjectReponsitory
    {
        Task CreateSubject(SubjectModel model);
        Task DeleteSubject(int Id);
        Task UpdateSubject(int Id,SubjectModel model);
        Task<List<SubjectDTO>> GetAll();// a-z
        Task<SubjectDTO> GetById(int Id);
        Task<List<SubjectDTO>> searchFilter(string? search,string? orderBy);//a-z lần truy cập gần nhất
    }
}
