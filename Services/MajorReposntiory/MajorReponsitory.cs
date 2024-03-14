using Library.Data;
using Library.DTO;
using Library.Model;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.MajorReposntiory
{
    public class MajorReponsitory : IMajorReponsitory
    {
        private readonly MyDB context;

        public MajorReponsitory(MyDB context)
        {
            this.context = context;
        }
        public async Task CreateMajor(MajorModel model)
        {
            var major = new Major
            {
                Name = model.Name,
                Code = model.Code,
            };
            await context.majors.AddAsync(major);
            await context.SaveChangesAsync();
        }

        public async Task DeleteMajor(int Id)
        {
            var major = await context.majors.FirstOrDefaultAsync(ma => ma.Id == Id);
            if (major != null)
            {
                context.Remove(major);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<MajorDTO>> GetAllMajor()
        {
            return await context.majors.Select(ma => new MajorDTO
            {
                Code = ma.Code,
                Id = ma.Id,
                Name = ma.Name,
            }).ToListAsync();
        }

        public async Task<MajorDTO> GetMajor(int Id)
        {
            var major = await context.majors.FirstOrDefaultAsync(ma => ma.Id == Id);
            if (major == null)
            {
                return null;
            }
            return new MajorDTO
            {
                Name = major.Name,
                Code = major.Code,
                Id = major.Id,
            };
        }

        public async Task UpdateMajor(int Id, MajorModel model)
        {
            var major = await context.majors.FirstOrDefaultAsync(ma => ma.Id == Id);
            if (major != null)
            {
                major.Name = model.Name;
                major.Code = model.Code;
                await context.SaveChangesAsync();
            }
        }
    }
}
