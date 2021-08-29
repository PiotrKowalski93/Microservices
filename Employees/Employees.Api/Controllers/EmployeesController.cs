using Employees.Domain;
using Employees.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Employees.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeesController : Controller
    {
        [HttpGet]
        public List<Employee> GetEmployees()
        {
            return new List<Employee>()
            {
                new Employee(){ Name = "test"}
            };
        }

        [HttpGet]
        public List<Employee> GlobalErrorHandler()
        {
            throw new GeneralException();
        }
    }
}
