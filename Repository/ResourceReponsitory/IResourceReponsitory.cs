using Library.Model;
using Library.Model.DTO;

namespace Library.Repository.ResourceReponsitory
{
    public interface IResourceReponsitory
    {
        Task CreateResource(ResourceModel model);
        Task AddResourceDocument(AddDocumnetResourceModel model);
        Task<DTOResource> GetResource(int id);
        Task<List<DTOResource>> GetAllResourceLesson(int LessonId);
    }
}
