﻿using System.ComponentModel.DataAnnotations;

namespace Library.Data
{
    public class Subject
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string SubjectCode { get; set; } = string.Empty;
        [Required] 
        public string Describe { get; set; } = string.Empty;    
        public ICollection<Topic>? topics { get; set; }
        public ICollection<Document>? documents { get; set; }
        public ICollection<SubjectClassRoom>? subjectClassRooms { get; set; }

    }
}