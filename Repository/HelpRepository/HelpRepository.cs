using Library.Data;
using Library.Model;
using Library.Model.DTO;
using Library.Services.JWTService;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.HelpRepository
{
    public class HelpRepository : IHelpRepository
    {
        private readonly MyDB context;
        private readonly IJWTSevice jWTSevice;

        public HelpRepository(MyDB context, IJWTSevice jWTSevice)
        {
            this.context = context;
            this.jWTSevice = jWTSevice;
        }
        public async Task CreateHelp(HelpModel model)
        {
            var userId = await jWTSevice.ReadToken();
            var help = new Help
            {
                Create_At = DateTime.Now,
                UserId = userId,
                Context = model.Context,
            };
            await context.helps.AddAsync(help);
            await context.SaveChangesAsync();
        }

        public async Task<List<HelpDTO>> GetAllHelp()
        {
            var helps = await context.helps.Include(f => f.applicationUsers).ToListAsync();
            return helps.Select(he => new HelpDTO
            {
                Id = he.Id,
                Create_at = he.Create_At,
                UserName = he.applicationUsers!.UserName,
                Context = he.Context,
            }).ToList();
        }

        public async Task<HelpDTO> GetHelpById(int Id)
        {
            var help = await context.helps.Include(f => f.applicationUsers).FirstOrDefaultAsync(he => he.Id == Id);
            if (help == null)
            {
                return null;
            };
            return new HelpDTO
            {
                Id = help.Id,
                Create_at = help.Create_At,
                UserName = help.applicationUsers!.UserName,
                Context = help.Context,
            };
        }
    }
}
