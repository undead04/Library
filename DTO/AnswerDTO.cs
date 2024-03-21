using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Library.DTO
{
    public class AnswerDTO
    {
        public string answer { get; set; } = string.Empty;
        public bool CorrectAnswer { get; set; }
    }
}
