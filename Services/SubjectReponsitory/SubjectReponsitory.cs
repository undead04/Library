using Library.Data;
using Library.DTO;
using Library.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using System.Security.Claims;

namespace Library.Services.SubjectReponsitory
{
    public class SubjectReponsitory : ISubjectReponsitory
    {
        private readonly MyDB context;


        public SubjectReponsitory(MyDB context)
        {
            this.context = context;


        }
        public async Task CreateSubject(SubjectModel model)
        {

            var subject = new Subject
            {

                Name = model.Name,
                SubjectCode = model.SubjectCode,
                Describe = model.Describe,
                MajorId=model.MajorId,

            };
            await context.AddAsync(subject);
            await context.SaveChangesAsync();
        }

        public async Task DeleteSubject(int Id)
        {
            var subject = await context.subjects.FirstOrDefaultAsync(su => su.Id == Id);
            if (subject != null)
            {
                context.Remove(subject);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<SubjectDTO>> GetAll()
        {
            var subjects = await context.subjects.Include(f=>f.Major).ToListAsync();
            return subjects.Select(su => new SubjectDTO
            {
                Id = su.Id,
                Name = su.Name,
                SubjectCode = su.SubjectCode,
                Describe = su.Describe,
                MajorId=su.MajorId,
                MajorName=su.Major!.Name
                
            }).ToList();
        }

        public async Task<SubjectDTO> GetById(int Id)
        {
            var subject = await context.subjects.Include(f=>f.Major).FirstOrDefaultAsync(su => su.Id == Id);
            if (subject != null)
            {
                return new SubjectDTO
                {
                    Id = subject.Id,
                    Name = subject.Name,
                    SubjectCode = subject.SubjectCode,
                    Describe = subject.Describe,
                    MajorName = subject.Major!.Name

                };
            }
            return null;

        }

        public Task<List<SubjectDTO>> searchFilter(string? search, string? orderBy)
        {
            var subject = context.subjects.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                subject = subject.Where(su => su.SubjectCode.Contains(search) || su.Name.Contains(search));
            }
            // lân truy cập gần nhất
            return subject.Select(su => new SubjectDTO
            {
                Id = su.Id,
                Name = su.Name,
                SubjectCode = su.SubjectCode,
                Describe = su.Describe,
                MajorName=su.Major!.Name
            }).ToListAsync();
        }

        public async Task UpdateSubject(int Id, SubjectModel model)
        {
            var subject = await context.subjects.FirstOrDefaultAsync(su => su.Id == Id);
            if (subject != null)
            {
                subject.Describe = model.Describe;
                subject.Name = model.Name;
                subject.SubjectCode = model.SubjectCode;
                await context.SaveChangesAsync();

            }
        }
        public async Task<List<SubjectDTO>> GetAllSubjectClass(int ClassId)
        {
            var subjects=await context.subjects.Include(f=>f.Major).Where(su=>su.subjectClassRooms.Any(su=>su.ClassRoomId==ClassId)).ToListAsync();
            return subjects.Select(su => new SubjectDTO
            {
                Id = su.Id,
                Name = su.Name,
                SubjectCode = su.SubjectCode,
                Describe = su.Describe,
                MajorName=su.Major.Name ,
            }).ToList();
        }
    }
}
