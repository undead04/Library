using Library.DTO;
using Library.Model;
namespace Library.Server.ResourceReponsitory
{
    public interface IResourceReponsitory
    {
        Task CreateResource(ResourceModel model);
        Task AddResourceDocument(AddDocumnetResourceModel model);
        Task<DTOResource> GetResource(int id);
        Task<List<DTOResource>> GetAllResourceLesson(int LessonId);
    }
}
