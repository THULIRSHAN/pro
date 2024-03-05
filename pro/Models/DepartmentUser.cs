using pro.Models;

public class DepartmentUser
{
    public string UserId { get; set; } // Foreign key for User
    public User User { get; set; }

    public int DepartmentID { get; set; } // Foreign key for Department
    public Department Department { get; set; }
}
