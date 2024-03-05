using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace pro.Models
{
    public class Resume
    {
        [Key]
        public int Resume_ID { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public string FilePath { get; set; }  // Add this property
        public string FileName { get; set; }  // Add this property
        public string Status { get; set; }  
        public DateTime Submission_date { get; set; }= DateTime.UtcNow;
        public DateTime Review_date { get; set; }
        public DateTime Selection_date { get; set; }

        public string Recruitment_remarks { get; set; } 



        [JsonIgnore]
        public User User { get; set; }
    }
}
