using Library.Data;
using Library.DTO;
using Library.Model;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.TopicReponsitory
{
    public class TopicReponsitory : ITopicReponsitory
    {
        private readonly MyDB context;

        public TopicReponsitory(MyDB context)
        {
            this.context = context;
        }
        public async Task CreateTopic(TopicModel model)
        {
            var topic = new Topic
            {
                Title = model.Title,
                SubjectId = model.SubjectId,
            };
            await context.topics.AddAsync(topic);
            await context.SaveChangesAsync();
        }

        public async Task DeleteTopic(int Id)
        {
            var topic = await context.topics.FirstOrDefaultAsync(to => to.Id == Id);
            if (topic != null)
            {
                context.topics.Remove(topic);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<TopicDTO>> GetAll(int subjectId)
        {
            var topics = await context.topics.Where(to => to.SubjectId == subjectId).ToListAsync();
            return topics.Select(to => new TopicDTO
            {
                Id = to.Id,
                Title = to.Title,
            }).ToList();
        }

        public async Task<TopicDTO> GetTopicById(int Id)
        {
            var topic = await context.topics.FirstOrDefaultAsync(to => to.Id == Id);
            if (topic != null)
            {
                return new TopicDTO { Id = topic.Id, Title = topic.Title };
            }
            return null;
        }

        public async Task UpdateTopic(int Id, TopicModel model)
        {
            var topic = await context.topics.FirstOrDefaultAsync(to => to.Id == Id);
            if (topic != null)
            {
                topic.Title = model.Title;
                await context.SaveChangesAsync();
            }
        }
    }
}
