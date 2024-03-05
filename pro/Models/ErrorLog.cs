using System;
using System.ComponentModel.DataAnnotations;

namespace pro.Models
{
    public class ErrorLog
    {
        [Key]
        public int Id { get; set; }
        public string ScreenName { get; set; }
        public string ErrorName { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

    }
}
