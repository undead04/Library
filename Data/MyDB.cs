using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class MyDB:IdentityDbContext<ApplicationUser>
    {
        public MyDB(DbContextOptions options) : base(options) { }
    }
}
