using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace pro.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

       
        [Required]
        public string DepartmentName { get; set; }


        public List<DepartmentUser> DepartmentUsers { get; set; }

    }
}
