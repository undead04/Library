using DocumentFormat.OpenXml.VariantTypes;
using Library.Data;
using Library.Model;
using Library.Model.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using System.Security.Claims;

namespace Library.Repository.SubjectReponsitory
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
                MajorId = model.MajorId,
                UserId = model.UserId,

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

        public async Task<List<SubjectDTO>> GetAll(string? search, int? subjectId, int? classId, string? orderBy, string? UserId, StatusDocument? statusDocument)
        {
            var subjects = context.subjects
                .Include(f => f.documents)
                .Include(f => f.Major)
                .Include(f => f.subjectClassRooms)
                .AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                subjects = subjects.Where(su => su.SubjectCode.Contains(search) || su.Name.Contains(search));
            }
            if (subjectId.HasValue)
            {
                subjects = subjects.Where(su => su.Id == subjectId);
            }
            if (!string.IsNullOrEmpty(UserId))
            {
                subjects = subjects.Where(su => su.UserId == UserId);
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                switch (orderBy)
                {
                    case "Name":
                        subjects = subjects.OrderBy(subjects => subjects.Name);
                        break;

                }
            }
            if (classId.HasValue)
            {
                subjects = subjects.Where(su => su.subjectClassRooms!.Any(cl => cl.ClassRoomId == classId));
            }
            if (statusDocument != null)
            {
                subjects = subjects.Where(su => su.documents!.Any(doc => doc.Status.Equals(statusDocument)));
            }
            return await subjects.Select(su => new SubjectDTO
            {
                Id = su.Id,
                Name = su.Name,
                SubjectCode = su.SubjectCode,
                Describe = su.Describe,
                MajorId = su.MajorId,
                MajorName = su.Major!.Name,
                StatusDocument = su.documents!.Count != 0 ? su.documents.Any(doc => doc.Status.Equals(StatusDocument.Wait)) ? StatusDocument.Wait.ToString() : StatusDocument.Complete.ToString() : string.Empty,
                Create_at = su.documents.Count != 0 ? su.documents.OrderByDescending(doc => doc.Create_at).FirstOrDefault()!.Create_at : DateTime.MinValue,
                TotalDoucment = su.documents.Count != 0 ? su.documents.Count() : 0,
                ApprovedDocuments = su.documents.Count != 0 ? su.documents.Where(doc => doc.Status.Equals(StatusDocument.Complete)).Count() : 0,
                Tearcher = su.ApplicationUser!.UserName,
                UserId = su.UserId


            }).ToListAsync();
        }

        public async Task<SubjectDTO> GetById(int Id)
        {
            var subject = await context.subjects.Include(f => f.Major).FirstOrDefaultAsync(su => su.Id == Id);
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



        public async Task UpdateSubject(int Id, SubjectModel model)
        {
            var subject = await context.subjects.FirstOrDefaultAsync(su => su.Id == Id);
            if (subject != null)
            {
                subject.Describe = model.Describe;
                subject.Name = model.Name;
                subject.SubjectCode = model.SubjectCode;
                subject.UserId = model.UserId;
                await context.SaveChangesAsync();

            }
        }
        public async Task<List<SubjectDTO>> GetAllSubjectClass(int ClassId)
        {
            var subjects = await context.subjects.Include(f => f.Major).Where(su => su.subjectClassRooms.Any(su => su.ClassRoomId == ClassId)).ToListAsync();
            return subjects.Select(su => new SubjectDTO
            {
                Id = su.Id,
                Name = su.Name,
                SubjectCode = su.SubjectCode,
                Describe = su.Describe,
                MajorName = su.Major.Name,
            }).ToList();
        }
    }
}
