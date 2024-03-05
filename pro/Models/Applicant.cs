using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace pro.Models
{
    public class Applicant
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } 

        public string Title { get; set; }

        public string Dob { get; set; }

        public string Gender { get; set; }

        public string PhoneNo { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Country { get; set; }

        [JsonIgnore]
        public User User { get; set; }


    }
}
