namespace pro.Models
{
    public class SkillUser
    {
        public string UserId { get; set; } // Foreign key for User
        public User User { get; set; }

        public int Skillid { get; set; } // Foreign key for Department
        public Skill Skill { get; set; }
    }
}
