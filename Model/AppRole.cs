namespace SchoolLibrary.Model
{
    public class AppRole
    {
        public const string Leader = "Quản lý";
        public const string Student = "Học viên";
        public const string Teacher = "Giảng viên";
        public string[] ArrayRole()
        {
            return new string[] { Leader, Student,Teacher };
        }
    }
}
