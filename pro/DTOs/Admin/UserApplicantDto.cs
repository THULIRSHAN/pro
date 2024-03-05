using pro.Models;
using System.Collections.Generic;
using System;

public class UserApplicantDto
{

    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Applicant Applicant { get; set; }
}
