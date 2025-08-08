using Aliance.Application.DTOs;
using Aliance.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aliance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _service.GetAllDepartments();

            return Ok(departments);
        }

        [HttpPost]
        public async Task<IActionResult> InsertDepartment([FromBody] DepartmentDTO department)
        {
                       if (department == null)
                return BadRequest();
            var createdDepartment = await _service.InsertDepartment(department);
            return Created("created", createdDepartment);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _service.GetDepartmentById(id);

            return Ok(department);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentDTO department)
        {
            if (department == null)
                return BadRequest();
                
            var updated = await _service.UpdateDepartment(department);

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var result = await _service.DeleteDepartment(id);

            return Ok(result);
        }
    }
}
