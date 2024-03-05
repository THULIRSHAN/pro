using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pro.DTOs.Inside
{
    public class DepartmentDTO
    {

        [Required]
        public int DepartmentID { get; set; }


        [Required]
        public string DepartmentName { get; set; }


    }
}
