using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace pro.Models
{
    public class User:IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
       
        public Applicant Applicant { get; set; }

        public List<Education> Educations { get; set; }
        public List<Resume> Resumes { get; set; }
        public List<SkillUser> SkillUsers { get; set; }
        public List<DepartmentUser> DepartmentUsers { get; set; }

        public Company Company { get; set; }

        public List<JobApplication> JobApplications { get; set; }
    }
}
