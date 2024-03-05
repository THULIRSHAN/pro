using pro.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace pro.Models
{
    public class Education
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public string CurrentStatus { get; set; }
        public string Qulification { get; set; }
        public string InsituteName { get; set; }
        public string Yearattained { get; set; }

        [JsonIgnore]
        public User User { get; set; }


    }
}

