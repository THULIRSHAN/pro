using System;
using System.ComponentModel.DataAnnotations;

namespace pro.DTOs.Inside
{
    public class CompanyDTO
    {
       
        public string CompanyName { get; set; }

       
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
