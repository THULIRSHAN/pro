using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace pro.DTOs.Inside
{
    public class ResumeDto
    {
       
        public string Status { get; set; }
        public DateTime Submission_date { get; set; } = DateTime.UtcNow;
      

    }
}
