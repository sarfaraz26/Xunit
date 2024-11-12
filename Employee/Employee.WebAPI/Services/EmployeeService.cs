using Employee.WebAPI.Models;

namespace Employee.WebAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private static List<EmployeeModel> _employees = new List<EmployeeModel>()
        {
            new EmployeeModel() { EmployeeId = new Guid("e7f2e8d5-0b93-4c68-9c67-1f5c3c8e3d1a"), Name = "Ravi", Salary = 20000},
            new EmployeeModel() { EmployeeId = new Guid("a3f2b3f0-4cde-4b62-a9f5-6c7c7e2a7e0b"), Name = "Ajay", Salary = 30000},
            new EmployeeModel() { EmployeeId = new Guid("fae1e4b6-9d2f-4e98-a1a6-b1f57e1f0e53"), Name = "Sani", Salary = 45000}
        };

        public EmployeeService()
        {

        }

        public async Task<List<EmployeeModel>> GetEmployees()
        {
            try
            {
                return await Task.FromResult(_employees);
            }
            catch
            {
                throw;
            }
        }

        public async Task<EmployeeModel> GetEmployeeById(Guid id)
        {
            try
            {
                var employee = _employees.Where(emp => emp.EmployeeId == id).SingleOrDefault();
                return await Task.FromResult(employee);
            }
            catch
            {
                throw;
            }
        }

        public async Task<EmployeeModel> AddEmployee(EmployeeModel employee)
        {
            try
            {
                _employees.Add(employee);
                var insertedEmployee = _employees.LastOrDefault();
                return await Task.FromResult(insertedEmployee);
            }
            catch
            {
                throw;
            }
        }

        public async Task<EmployeeModel> DeleteEmployee(Guid id)
        {
            try
            {
                var foundEmployee = await GetEmployeeById(id);

                if (foundEmployee != null)
                    _employees.Remove(foundEmployee);

                return await Task.FromResult(foundEmployee);

            }
            catch
            {
                throw;
            }
        }

        public async Task<EmployeeModel> UpdateEmployee(Guid id, EmployeeModel employee)
        {
            try
            {
                var employeeFound = await GetEmployeeById(id);

                if (employeeFound != null)
                {
                    employeeFound.Name = employee.Name;
                    employeeFound.Salary = employee.Salary;
                }

                return employeeFound;
            }
            catch
            {
                throw;
            }
        }
    }
}
