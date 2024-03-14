using Library.DTO;
using Library.Model;

namespace Library.Services.TopicReponsitory
{
    public interface ITopicReponsitory
    {
        Task CreateTopic(TopicModel model);
        Task DeleteTopic(int Id);
        Task UpdateTopic(int Id, TopicModel model);
        Task<TopicDTO> GetTopicById(int Id);
        Task<List<TopicDTO>> GetAll(int subjectId);

    }
}
