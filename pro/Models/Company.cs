using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace pro.Models
{
    public class Company
    {
        [Key]
        public int Company_ID { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        
    }
}
