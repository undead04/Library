namespace SchoolLibrary.Model
{
    public class AppRole
    {
        public const string Leader = "leader";
        public const string Student = "student";
        public const string Teacher = "teacher";
        public string[] ArrayRole()
        {
            return new string[] { Leader, Student,Teacher };
        }
    }
}
