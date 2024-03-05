using System.ComponentModel.DataAnnotations;

namespace pro.DTOs.Inside
{
    public class DepartmentUserDTO
    {
        [Required]
        public int DepartmentID { get; set; }
    }
}
