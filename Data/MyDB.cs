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
       
        public DbSet<ClassRoom> classRooms { get; set; } 
        public DbSet<SubjectClassRoom> subjectClassRooms { get; set; }
        public DbSet<Tearcher> tearchers { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Major> majors { get; set; }
        public DbSet<Resources> resources { get; set; }
        public DbSet<Answer> answers { get;set ; }
        public DbSet<Exam> exams { get; set; }
        public DbSet<Help> helps { get; set; }
        public DbSet<NotificationClassRoom> notificationClassRooms { get; set; }
        public DbSet<NotificationSubject> notificationSubjects { get; set; }
        public DbSet<Question> questions { get; set; }
        public DbSet<QuestionClassRoom> questionClassRooms { get; set; }
        public DbSet<QuestionExam> questionExams { get; set; }
        public DbSet<QuestionSubject> questionSubjects { get; set; }
        public DbSet<ReplyQuestion> replyQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(e =>
            {
                
                e.HasOne(e => e.Tearcher)
                .WithOne(e => e.user)
                .HasForeignKey<Tearcher>(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasOne(e => e.Student)
                .WithOne(e => e.user)
                .HasForeignKey<Student>(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasMany(e => e.helps)
                .WithOne(e => e.applicationUsers)
                .HasForeignKey(e => e.UserId);
                e.HasMany(e => e.exams)
                .WithOne(e => e.applicationUsers)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasMany(e => e.questions)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.CreateUserId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasMany(e => e.questionSubjects)
                .WithOne(e => e.ApplicationUser)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                e.HasMany(e => e.notificationSubjects)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
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
                e.HasOne(e => e.Major)
                .WithMany(e => e.Subjects)
                .HasForeignKey(e => e.MajorId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasMany(e => e.notificationSubjects)
                .WithOne(e => e.Subject)
                .HasForeignKey(e => e.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);
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
                e.HasMany(e => e.questionSubjects)
                .WithOne(e => e.Lesson)
                .HasForeignKey(e => e.LessonId)
                .OnDelete(DeleteBehavior.NoAction);
                
            });
            builder.Entity<Document>(e => {
                e.HasMany(x => x.resources)
                .WithOne(x => x.Document)
                .HasForeignKey(x => x.DoucmentId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasOne(e => e.subject)
                .WithMany(e => e.documents)
                .HasForeignKey(e => e.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);
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
            builder.Entity<QuestionClassRoom>(e =>
            {
                e.HasOne(e => e.QuestionSubject)
                .WithMany(e => e.questionClassRooms)
                .HasForeignKey(e => e.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasOne(e => e.ClassRoom)
               .WithMany(e => e.questionClassRooms)
               .HasForeignKey(e => e.ClassRoomId)
               .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<NotificationClassRoom>(e =>
            {
                e.HasOne(e => e.ClassRoom)
                .WithMany(e => e.notificationClassRooms)
                .HasForeignKey(e => e.ClassRoomId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasOne(e => e.NotificationSubject)
               .WithMany(e => e.notificationClassRooms)
               .HasForeignKey(e => e.SubjectNotificationid)
               .OnDelete(DeleteBehavior.NoAction);
            });
            
            builder.Entity<QuestionExam>(e =>
            {
                e.HasOne(e => e.Exam)
                .WithMany(e => e.QuestionExams)
                .HasForeignKey(e => e.Examid)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasOne(e => e.Question)
                .WithMany(e => e.questionExams)
                .HasForeignKey(e => e.Examid)
                .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<Question>(e =>
            {
                e.HasMany(e => e.Answers)
                .WithOne(e => e.Question)
                .HasForeignKey(e => e.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);
            });






        }
    }
}
