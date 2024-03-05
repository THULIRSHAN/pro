using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pro.Data;
using pro.DTOs.Inside;
using pro.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace pro.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly Context _context;
       

        public DepartmentController(Context context) // Add UserManager to the constructor
        {
            _context = context;
           
        }

       
        [HttpPost("depart")]
        public async Task<ActionResult<Department>> CreateDepartment(DepartmentDTO departmentDTO)
        {
     

          

            // Create a new Applicant
            var newDepartment = new Department
            {

               DepartmentID=departmentDTO.DepartmentID,
               DepartmentName=departmentDTO.DepartmentName,


            };

            // Add the new department to the context
            _context.Departments.Add(newDepartment);


            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok(newDepartment);
        }
    }
}
