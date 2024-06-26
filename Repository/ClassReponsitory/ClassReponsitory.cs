﻿using Library.Data;
using Library.Model;
using Library.Model.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Library.Repository.ClassReponsitory
{
    public class ClassReponsitory : IClassReponsitory
    {
        private readonly MyDB context;
        private readonly UserManager<ApplicationUser> userManager;

        public ClassReponsitory(MyDB context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task CreateClass(ClassRoomModel model)
        {
            var classRoom = new ClassRoom
            {
                Name = model.Name,
                CodeClassRoom = model.CodeClassRoom,

            };
            await context.classRooms.AddAsync(classRoom);
            await context.SaveChangesAsync();
        }

        public async Task DeleteClass(int Id)
        {
            var classRoom = await context.classRooms.FirstOrDefaultAsync(cl => cl.Id == Id);
            if (classRoom != null)
            {
                context.classRooms.Remove(classRoom);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<ClassRoomDTO>> GetAllClass()
        {



            return await context.classRooms.Select(cl => new ClassRoomDTO
            {
                Id = cl.Id,
                Name = cl.Name,
                CodeClassRoom = cl.CodeClassRoom,
            }).ToListAsync();
        }

        public async Task<ClassRoomDTO> GetById(int Id)
        {
            var classRoom = await context.classRooms.FirstOrDefaultAsync(cl => cl.Id == Id);
            if (classRoom != null)
            {
                return new ClassRoomDTO
                {
                    Id = classRoom.Id,
                    Name = classRoom.Name,
                    CodeClassRoom = classRoom.CodeClassRoom
                };
            }
            return null;
        }

        public async Task UpdateClass(int Id, ClassRoomModel model)
        {
            var classRoom = await context.classRooms.FirstOrDefaultAsync(cl => cl.Id == Id);
            if (classRoom != null)
            {
                classRoom.CodeClassRoom = model.CodeClassRoom;
                classRoom.Name = model.Name;
                await context.SaveChangesAsync();
            }
        }
        public async Task AddSubjectClass(AddTearcherClassRoom model)
        {
            var subjectRoom = new SubjectClassRoom
            {
                SubjectId = model.SubjectId,
                ClassRoomId = model.ClassRoomId,

            };
            await context.subjectClassRooms.AddAsync(subjectRoom);
            await context.SaveChangesAsync();
        }
        public async Task<List<ClassRoomDTO>> GetAllCLassRoomSubject(int subjectId)
        {
            var classRooms = await context.classRooms.Where(cl => cl.subjectClassRooms.Any(cl => cl.SubjectId == subjectId)).ToListAsync();
            return classRooms.Select(cl => new ClassRoomDTO
            {
                Id = cl.Id,
                Name = cl.Name,
                CodeClassRoom = cl.CodeClassRoom,
            }).ToList();
        }


    }
}
