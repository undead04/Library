using Library.Data;
using Library.DTO;
using Library.Model;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.HelpRepository
{
    public class HelpRepository : IHelpRepository
    {
        private readonly MyDB context;

        public HelpRepository(MyDB context) 
        {
            this.context = context;
        }
        public async Task CreateHelp(HelpModel model)
        {
            var help = new Help
            {
                Create_At = DateTime.Now,
                UserId= model.UserId,
                Context=model.Context,
            };
            await context.helps.AddAsync(help);
            await context.SaveChangesAsync();
        }

        public async Task<List<HelpDTO>> GetAllHelp()
        {
            var helps = await context.helps.Include(f=>f.applicationUsers).ToListAsync();
            return helps.Select(he => new HelpDTO
            {
                Id = he.Id,
                Create_at=he.Create_At,
                UserName=he.applicationUsers!.UserName,
                Context=he.Context,
            }).ToList();
        }

        public async Task<HelpDTO> GetHelpById(int Id)
        {
            var help = await context.helps.Include(f=>f.applicationUsers).FirstOrDefaultAsync(he => he.Id == Id);
            if(help==null)
            {
                return null;
            };
            return new HelpDTO
            {
                Id = help.Id,
                Create_at=help.Create_At,
                UserName=help.applicationUsers!.UserName,
                Context=help.Context,
            };
        }
    }
}
