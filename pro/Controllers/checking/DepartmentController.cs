using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pro.Data; // Assuming your DbContext is in the pro.Data namespace
using pro.DTOs.Inside;
using pro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly Context _context;

        public DepartmentController(Context context)
        {
            _context = context;
        }

        [HttpGet("GetDepartments")]
        public ActionResult<IEnumerable<Department>> GetDepartments()
        {
            var departments = _context.Departments.ToList();
            return Ok(departments);
        }

        [HttpPost("CreateDepartment")]
        public async Task<ActionResult<DepartmentDTO>> CreateDepartment(DepartmentDTO departmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var department = new Department
            {
                DepartmentName = departmentDTO.DepartmentName
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            // Return the newly created skill
            return Ok(department);

        }

        [HttpPut("UpdateDepartment/{id}")]
        public async Task<ActionResult<Department>> UpdateDepartment(int id, DepartmentDTO updatedDepartmentDTO)
        {
            var departmentToUpdate = await _context.Departments.FindAsync(id);

            if (departmentToUpdate == null)
            {
                return NotFound();
            }

            departmentToUpdate.DepartmentName = updatedDepartmentDTO.DepartmentName;

            await _context.SaveChangesAsync();

            return Ok(departmentToUpdate);
        }

        [HttpDelete("DeleteDepartment/{id}")]
        public async Task<ActionResult> DeleteDepartment(int id)
        {
            try
            {
                var departmentToDelete = await _context.Departments.FindAsync(id);

                if (departmentToDelete == null)
                {
                    return NotFound();
                }

                _context.Departments.Remove(departmentToDelete);

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return StatusCode(500, $"Error deleting department: {ex.Message}");
            }
        }



    }
}
