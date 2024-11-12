using Employee.WebAPI.Models;
using Employee.WebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace Employee.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService) 
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route("GetEmployees")]
        public async Task<ActionResult<List<EmployeeModel>>> GetEmployees()
        {
            try
            {
                var employees = await _employeeService.GetEmployees();

                if (employees == null)
                    return NotFound("Something went wrong");

                return Ok(employees);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetEmployees/{id}")]
        public async Task<ActionResult<EmployeeModel>> GetEmployeeById(Guid id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeById(id);

                if (employee == null)
                    return NotFound($"Invalid EmployeeId - {id}");

                return Ok(employee);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("AddEmployee")]
        public async Task<ActionResult<EmployeeModel>> AddEmployee(EmployeeModel newEmployee)
        {
            try
            {
                var employeeAdded = await _employeeService.AddEmployee(newEmployee);

                if (employeeAdded == null)
                    return NotFound("Something went wrong");

                return Ok(employeeAdded);
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteEmployee/{id}")]
        public async Task<ActionResult<EmployeeModel>> DeleteEmployee(Guid id)
        {
            try
            {
                var deletedEmployee = await _employeeService.DeleteEmployee(id);

                if (deletedEmployee == null)
                    return NotFound("Something went wrong");

                return Ok(deletedEmployee);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut]
        [Route("UpdateEmployee/{id}")]
        public async Task<ActionResult<EmployeeModel>> UpdateEmployee(Guid id, EmployeeModel employeeNewObj)
        {
            try
            {
                var updatedEmployee = await _employeeService.UpdateEmployee(id, employeeNewObj);

                if (updatedEmployee == null)
                    return NotFound("Something went wrong");

                return Ok(updatedEmployee);
            }
            catch
            {
                throw;
            }
        }
    }
}
