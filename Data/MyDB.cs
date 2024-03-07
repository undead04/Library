using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class MyDB:IdentityDbContext<ApplicationUser>
    {
        public MyDB(DbContextOptions options) : base(options) { }
        public DbSet<Role>roles { get; set; }
        public DbSet<Subject>subjects { get; set; }
        public DbSet<Topic>topics { get; set; }
        public DbSet<Lesson> lessons { get; set; }
        public DbSet<Document> documents { get; set; }
        public DbSet<Approve> approves { get; set; }  
        public DbSet<ClassRoom> classRooms { get; set; } 
        public DbSet<SubjectClassRoom> subjectClassRooms { get; set; }
        public DbSet<Tearcher> tearchers { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Major> majors { get; set; }
        public DbSet<Resources> resources { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(e =>
            {
                e.HasOne(e => e.Role)
                .WithMany(e => e.User)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasOne(e => e.Tearcher)
                .WithOne(e => e.user)
                .HasForeignKey<Tearcher>(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasOne(e => e.Student)
                .WithOne(e => e.user)
                .HasForeignKey<Student>(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<Major>(e =>
            {
                e.HasMany(e => e.students)
                .WithOne(e => e.major)
                .HasForeignKey(e => e.MajorId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasMany(e => e.tearchers)
                .WithOne(e => e.major)
                .HasForeignKey(e => e.MajorId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<Subject>(e =>
            {
                e.HasMany(x => x.topics)
                 .WithOne(x => x.Subject)
                 .HasForeignKey(x => x.SubjectId)
                 .OnDelete(DeleteBehavior.NoAction);
                e.HasMany(e => e.documents)
                .WithOne(e => e.subject)
                .HasForeignKey(e => e.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<Lesson>(e =>
            {
                e.HasOne(e => e.topic)
                .WithMany(e => e.lessons)
                .HasForeignKey(e => e.TopicId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasMany(e => e.Resources)
                .WithOne(e => e.Lesson)
                .HasForeignKey(e => e.LessonId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasOne(e => e.Document)
                .WithMany(e => e.lessons)
                .HasForeignKey(e => e.DoucumentId)
                .OnDelete(DeleteBehavior.NoAction);
                
                
            });
            builder.Entity<Document>(e => {
                e.HasMany(x => x.resources)
                .WithOne(x => x.Document)
                .HasForeignKey(x => x.DoucmentId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasOne(e=>e.Approve)
                .WithOne(x=>x.document)
                .HasForeignKey<Approve>(e=>e.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<SubjectClassRoom>(e =>
            {
                e.HasOne(x => x.Subject)
                .WithMany(x => x.subjectClassRooms)
                .HasForeignKey(x => x.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasOne(x => x.ClassRoom)
               .WithMany(x => x.subjectClassRooms)
               .HasForeignKey(x => x.ClassRoomId)
               .OnDelete(DeleteBehavior.NoAction);
                e.HasOne(e => e.Tearcher)
                .WithMany(e => e.subjectClassRooms)
                .HasForeignKey(e => e.TearcherId)
                .OnDelete(DeleteBehavior.NoAction);
            });
           





        }
    }
}
