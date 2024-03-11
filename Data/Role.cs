using Microsoft.AspNetCore.Identity;
using MimeKit.Encodings;
using System.ComponentModel.DataAnnotations;

namespace Library.Data
{
    public class Role:IdentityRole
    {
        
        public string Description { get; set; } = string.Empty;
        public DateTime Create_at { get;set; }

    }
}
