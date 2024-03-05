using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace pro.DTOs.Inside
{

    public class Edu
    {
  
        public SkillUserDTO SkillUserDTO { get; set; }
        public EducationDTO EducationDTO { get; set; }
        public DepartmentUserDTO DepartmentUserDTO { get; set; }


    }

    public class SkillUserDTO
    {
        [Required]
        public List<int> SoftSkill { get; set; }

        [Required]
        public List<int> HardSkill { get; set; }

        [Required]
        public List<int> Language { get; set; }
    }
}



