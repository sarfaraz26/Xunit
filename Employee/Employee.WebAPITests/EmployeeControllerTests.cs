using Employee.WebAPI.Controllers;
using Employee.WebAPI.Services;
using Employee.WebAPI.Models;
using Moq;
using System.Runtime.CompilerServices;
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

        #region GetEmployees() Tests
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

        #region GetEmployeeById() Tests
        #endregion

        #region CreateEmployee() Tests
        #endregion

        #region UpdateEmployee() Tests
        #endregion

        #region DeleteEmployee() Tests
        #endregion

    }
}
