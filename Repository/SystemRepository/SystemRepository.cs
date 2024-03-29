using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using Library.Data;
using Library.Model;
using Library.Model.DTO;
using Library.Services.UploadService;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository.SystemRepository
{
    public class SystemRepository : ISystemRepository
    {
        private readonly MyDB context;
        private readonly IUploadService uploadService;

        public SystemRepository(MyDB context,IUploadService uploadService) 
        { 
            this.context=context;
            this.uploadService = uploadService;
        }
        public async Task CreateSystem(SystemModel model)
        {
            var system = new Data.System
            {
                CodeSchool = model.CodeSchool,
                Address=model.Address,
                Email=model.Email,
                NameSchool=model.NameSchool,
                NameSystemLibrary=model.NameSystemLibrary,
                Phone=model.Phone,
                Principal=model.Principal,
                TypeSchool=model.TypeSchool,
                Website=model.Website,
                
            };
            if(model.Logo != null)
            {
                if (model.Logo.Length > 0)
                {
                   system.Logo=await uploadService.UploadImage("Logo", model.Logo);
                }
            }
            await context.systems.AddAsync(system);
            await context.SaveChangesAsync();
        }

        public async Task<SystemDTO> GetSystem()
        {
            var system = await context.systems.FirstOrDefaultAsync();
            if (system == null)
            {
                return null;
            }
            return new SystemDTO
            {

                Address = system.Address,
                CodeSchool = system.CodeSchool,
                Email = system.Email,
                Logo = system.Logo,
                NameSchool = system.NameSchool,
                NameSystemLibrary = system.NameSystemLibrary,
                Phone = system.Phone,
                Principal = system.Principal,
                TypeSchool = system.TypeSchool,
                Website = system.Website,
                UrlLogo = uploadService.GetUrlImage(system.Logo, "Logo")
            };
        }

        public async Task UpdateSystem(SystemModel model)
        {
            var system = await context.systems.FirstOrDefaultAsync();
            if(system!=null)
            {
                system.Address = model.Address;
                system.CodeSchool = model.CodeSchool;
                system.Email = model.Email;
                system.NameSchool = model.NameSchool;
                system.NameSystemLibrary = model.NameSystemLibrary;
                system.Phone = model.Phone;
                system.Principal = model.Principal;
                system.TypeSchool = model.TypeSchool;
                system.Website = model.Website;
                if(model.Logo!=null)
                {
                    if (model.Logo.Length>0)
                    {
                        uploadService.DeleteImage("Logo", system.Logo);
                        system.Logo = await uploadService.UploadImage("Logo", model.Logo);

                    }
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
