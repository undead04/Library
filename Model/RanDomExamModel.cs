namespace Library.Model
{
    public class RanDomExamModel
    {
        public string Name { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public int QuatityExam { get; set; }
        public int Point { get; set; }
        public int QuantityQuestion { get; set; }
        public int QuantityEasy { get; set; }
        public int QuantityMedium { get; set; }
        public int QuatityDifficult { get; set; }
    }
}
