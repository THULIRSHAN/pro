using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace pro.Models
{
    public class JobApplication
    {
        [Key]
        public int AppId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        // Desired Location
        public string DesiredLocation { get; set; }

        // Are you looking for a full-time permanent position?
        public bool IsFullTimePosition { get; set; }

        // When are you ready to start?
        public DateTime? StartDate { get; set; }

        // Where did you hear about this opportunity?
        public string Source { get; set; }

        // Preferred Contact Method
        public string PreferredContactMethod { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }

   
}
