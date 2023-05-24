using FullStack.API.DatabaseContext;
using FullStack.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        ApplicationContext db;

        public EmployeeController(ApplicationContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = db.Employees.ToList();
            return Ok(data);
        }
        [HttpGet("{id:Guid}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                Employee employee = db.Employees.Find(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return Ok(employee);
            }
            catch (Exception)
            {
                return NotFound($"Employee with id: {id} does not exists");
            }


        }
        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            try
            {
                employee.Id = Guid.NewGuid();
                db.Employees.Add(employee);
                db.SaveChanges();

                return StatusCode(StatusCodes.Status201Created, employee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{id:Guid}")]
        public IActionResult Update(Guid id, Employee employee)
        {
            try
            {
                Employee emp = db.Employees.Where(x => x.Id == id).FirstOrDefault();

                if (emp.Id != null)
                {
                    emp.Name = employee.Name;
                    emp.Email = employee.Email;
                    emp.Phone = employee.Phone;
                    emp.Salary = employee.Salary;
                    emp.Department = employee.Department;

                    db.Employees.Update(emp);
                    db.SaveChanges();
                    return StatusCode(StatusCodes.Status201Created, employee);
                }
                return NotFound($"Employee with id: {id} does not exists");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
