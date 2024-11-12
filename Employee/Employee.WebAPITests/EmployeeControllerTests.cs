using Employee.WebAPI.Controllers;
using Employee.WebAPI.Services;
using Employee.WebAPI.Models;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace Employee.WebAPITests
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeService> _employeeServiceMock;
        private readonly EmployeeController _employeeController;

        public EmployeeControllerTests()
        {
            _employeeServiceMock = new Mock<IEmployeeService>();
            _employeeController = new EmployeeController(_employeeServiceMock.Object);
        }

        #region GetEmployees Tests
        [Fact]
        public async void GetEmployees_Returns_OkResult_WithEmployees()
        {
            //Arrange
            var employeesData = new List<EmployeeModel>
            {
                new EmployeeModel() { EmployeeId = Guid.NewGuid(), Name = "Sun", Salary = 2000},
                new EmployeeModel() { EmployeeId = Guid.NewGuid(), Name = "Mon", Salary = 3000}
            };

            _employeeServiceMock.Setup(service => service.GetEmployees()).Returns(Task.FromResult(employeesData));


            //Act
            var result = await _employeeController.GetEmployees();


            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<List<EmployeeModel>>(okObjectResult.Value);
        }

        [Fact]
        public async void GetEmployees_Returns_OkResult_WithNoEmployees()
        {
            //Arrange
            _employeeServiceMock.Setup(service => service.GetEmployees()).Returns(Task.FromResult(new List<EmployeeModel>()));

            //Act
            var result = await _employeeController.GetEmployees();

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var fetchedEmployees = Assert.IsType<List<EmployeeModel>>(okObjectResult.Value);
            Assert.Empty(fetchedEmployees);
        }

        [Fact]
        public async void GetEmployees_Returns_NotFound_WhenFetchedNull()
        {
            //Arrange
            List<EmployeeModel>? employees = null;
            _employeeServiceMock.Setup(service => service.GetEmployees()).Returns(Task.FromResult(employees));

            //Act
            var result = await _employeeController.GetEmployees();

            //Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.IsType<string>(notFoundResult.Value);
        }
        #endregion

        #region GetEmployeeById Tests
        [Fact]
        public async Task GetEmployeeById_Returns_OkObjectResult_WithEmployee()
        {
            //Arrange
            Guid employeeId = Guid.NewGuid();

            EmployeeModel employee = new EmployeeModel()
            {
                EmployeeId = employeeId,
                Name = "Sani",
                Salary = 4200
            };

            _employeeServiceMock.Setup(service => service.GetEmployeeById(employeeId)).Returns(Task.FromResult(employee));


            //Act
            var result = await _employeeController.GetEmployeeById(employeeId);


            //Assert
            var okObjResult = Assert.IsType<OkObjectResult>(result.Result);
            var obj = Assert.IsType<EmployeeModel>(okObjResult.Value);
            Assert.Equal(obj.EmployeeId, employeeId);
            Assert.Equal(obj.Name, employee.Name);
            Assert.Equal(obj.Salary, employee.Salary);
        }

        [Fact]
        public async Task GetEmployeeById_Returns_NotFound_WhenPassed_NonExisting_EmployeeId()
        {
            //Arrange
            Guid nonExisitinEmployeeId = Guid.NewGuid();
            EmployeeModel? nullEmployee = null;

            _employeeServiceMock.Setup(service => service.GetEmployeeById(nonExisitinEmployeeId)).ReturnsAsync(nullEmployee);


            //Act
            var result = await _employeeController.GetEmployeeById(nonExisitinEmployeeId);


            //Assert
            var notFoundObj = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(notFoundObj.StatusCode, 404);
            Assert.Equal(notFoundObj?.Value?.ToString(), $"Invalid EmployeeId - {nonExisitinEmployeeId}");
        }
        #endregion

        #region CreateEmployee Tests

        #endregion

        #region UpdateEmployee Tests
        [Fact]
        public async Task UpdateEmployee_Returns_OkResult_WhenPassed_ExisitingEmployeeId()
        {
            //Arrange
            Guid employeeId = Guid.NewGuid();
            EmployeeModel empUpdateObj = new()
            {
                Name = "Ray",
                Salary = 300
            };

            EmployeeModel response = new()
            {
                EmployeeId = employeeId,
                Name = empUpdateObj.Name,
                Salary = empUpdateObj.Salary
            };

            _employeeServiceMock.Setup(service => service.UpdateEmployee(employeeId, empUpdateObj)).ReturnsAsync(response);


            //Act
            var result = await _employeeController.UpdateEmployee(employeeId,empUpdateObj);


            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(okObjectResult.StatusCode, 200);
            var responseVal = Assert.IsType<EmployeeModel>(okObjectResult.Value);
            Assert.True(responseVal.Name == empUpdateObj.Name);
            Assert.True(responseVal.Salary == empUpdateObj.Salary);
            Assert.True(responseVal.EmployeeId == employeeId);
        }
        #endregion

        #region DeleteEmployee Tests
        [Fact]
        public async Task DeleteEmployee_Returns_OkObjectResult_WhenPassed_Valid_EmployeeId()
        {
            //Arrange
            Guid employeeId = Guid.NewGuid();
            EmployeeModel deletedEmployee = new EmployeeModel()
            {
                EmployeeId = employeeId,
                Name = "Ashu",
                Salary = 1200
            };

            _employeeServiceMock.Setup(service => service.DeleteEmployee(employeeId)).ReturnsAsync(deletedEmployee);


            //Act
            var result = await _employeeController.DeleteEmployee(employeeId);


            //Assert
            var okObjResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(okObjResult.StatusCode, 200);

            var deletedEmpResult = Assert.IsType<EmployeeModel>(okObjResult.Value);
            Assert.Equal(deletedEmpResult.EmployeeId, deletedEmployee.EmployeeId);
            Assert.Equal(deletedEmpResult.Name, deletedEmployee.Name);
            Assert.Equal(deletedEmpResult.Salary, deletedEmployee.Salary);
        }

        [Fact]
        public async Task DeleteEmployee_Returns_NotFound_WhenPassed_Invalid_EmployeeId()
        {
            //Arrange
            Guid employeeId = Guid.NewGuid();
            EmployeeModel? deletedEmployee = null;
            _employeeServiceMock.Setup(service => service.DeleteEmployee(employeeId)).ReturnsAsync(deletedEmployee);


            //Act
            var result = await _employeeController.DeleteEmployee(employeeId);


            //Assert
            var notFoundObjResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(notFoundObjResult.StatusCode, 404);
            Assert.Equal(notFoundObjResult.Value, "Something went wrong");
        }
        #endregion

    }
}
