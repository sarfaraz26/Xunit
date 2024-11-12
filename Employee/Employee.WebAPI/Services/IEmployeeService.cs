
using Employee.WebAPI.Models;

namespace Employee.WebAPI.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeModel>> GetEmployees();
        Task<EmployeeModel> GetEmployeeById(Guid id);
        Task<EmployeeModel> AddEmployee(EmployeeModel employee);
        Task<EmployeeModel> DeleteEmployee(Guid id);
        Task<EmployeeModel> UpdateEmployee(Guid id, EmployeeModel employee);
    }
}
