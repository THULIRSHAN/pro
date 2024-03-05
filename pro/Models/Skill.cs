using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace pro.Models
{
    public class Skill
    {
        [Key]
        public int Skillid { get; set; }

        [Required]
        public int SkillType { get; set; }
        [Required]
        public string SkillName { get; set; }

        public List<SkillUser> SkillUsers { get; set; }



    }
}
