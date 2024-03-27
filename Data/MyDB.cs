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
        public DbSet<ClassLesson> classLessons { get; set; }
        public DbSet<ClassResource> classResources { get; set; }
        public DbSet<EssayExam> essayExams { get; set; }
        public DbSet<HistoryLike> historyLikes { get; set; }
        public DbSet<PrivateFile> privateFiles { get; set; }
        public DbSet<SystemNotification> systemNotifications { get; set; }
        public DbSet<Notification> notifications { get; set; }
        public DbSet<MyNotification> myNotifications { get; set; }
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
                e.HasMany(e => e.documents)
                .WithOne(e => e.ApplicationUser)
                .HasForeignKey(e => e.ApprovedByUserId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasMany(e => e.privateFiles)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.CreateUserId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasMany(e => e.HistoryLikes)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasOne(e=>e.systemNotifications)
                .WithOne(e=>e.User)
                .HasForeignKey<SystemNotification>(e => e.UserId)
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
                e.HasOne(e => e.Major)
                .WithMany(e => e.Subjects)
                .HasForeignKey(e => e.MajorId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasMany(e => e.notificationSubjects)
                .WithOne(e => e.Subject)
                .HasForeignKey(e => e.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);
                e.HasMany(e => e.exams)
                .WithOne(e => e.Subject)
                .HasForeignKey(e => e.Subjectid)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasOne(e => e.ApplicationUser)
                .WithMany(e => e.Subjects)
                .HasForeignKey(e => e.UserId)
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
                e.HasOne(e => e.ApplicationUser)
                .WithMany(e => e.documents)
                .HasForeignKey(e => e.CreateUserId)
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
                .HasForeignKey(e => e.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<Question>(e =>
            {
                e.HasMany(e => e.Answers)
                .WithOne(e => e.Question)
                .HasForeignKey(e => e.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<ClassLesson>(e =>
            {
                e.HasOne(e => e.Lesson)
                .WithMany(e => e.ClassLessons)
                .HasForeignKey(e => e.LessonId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasOne(e => e.ClassRoom)
                .WithMany(e => e.ClassLessons)
                .HasForeignKey(e => e.ClassId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<QuestionSubject>(e =>
            {
                e.HasMany(e => e.replyQuestions)
                .WithOne(e => e.QuestionSubject)
                .HasForeignKey(e => e.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasMany(e => e.historyLikes)
                .WithOne(e => e.QuestionSubject)
                .HasForeignKey(e => e.SubjectQuestionId)
                .OnDelete(DeleteBehavior.NoAction);
                
            });
            builder.Entity<ReplyQuestion>(e =>
            {
                e.HasOne(e => e.User)
                .WithMany(e => e.replyQuestions)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<Exam>(e =>
            {
                e.HasMany(e => e.essayExams)
                .WithOne(e => e.Exam)
                .HasForeignKey(e => e.ExamId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<ClassResource>(e =>
            {
                e.HasOne(e => e.ClassRoom)
                .WithMany(e => e.classResources)
                .HasForeignKey(e => e.ClassId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasOne(e => e.Resources)
                .WithMany(e => e.ClassResources)
                .HasForeignKey(e => e.ResourceId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            builder.Entity<HistoryLike>(e =>
            {
                e.HasKey(e => new { e.UserId, e.SubjectQuestionId });
            });
            builder.Entity<MyNotification>(e =>
            {
                e.HasKey(e => new { e.UserId, e.NotificationId });
                e.HasOne(e => e.User)
                .WithMany(e => e.myNotifications)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);
                e.HasOne(e => e.Notification)
                .WithMany(e => e.myNotifications)
                .HasForeignKey(e => e.NotificationId)
                .OnDelete(DeleteBehavior.NoAction);

            });




        }
    }
}
