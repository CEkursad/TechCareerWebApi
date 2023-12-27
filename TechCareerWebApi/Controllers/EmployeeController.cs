using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using TechCareerWebApi.ORM;

namespace TechCareerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly TechCareerDbContext _context;

        public EmployeeController()
        {
            _context = new TechCareerDbContext();
        }

        [HttpGet]
        public IActionResult Get()
        {
            //databasedeki blogları getir
            var employee = _context.Employees.ToList();
            return Ok(employee);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //disaridan gelen id ile blogu bul
            //find metodu primary keye göre arama yapar
            //var blog = _context.Blogs.Find(id);

            //firstordefault metodu icerisine yazdığımız sorguya göre arama yapar
            var employee = _context.Employees.FirstOrDefault(x => x.Id == id);

            //blog yoksa 404 döndür
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return StatusCode(201, employee);
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee updatedEmployee)
        {
            
            var existingEmployee = _context.Employees.FirstOrDefault(e => e.Id == id);

            if (existingEmployee == null)
            {
                return NotFound();
            }

            // Sadece belirli alanları güncelle
            existingEmployee.FirstName = string.IsNullOrWhiteSpace(updatedEmployee.FirstName) ? existingEmployee.FirstName : updatedEmployee.FirstName;
            existingEmployee.LastName = string.IsNullOrWhiteSpace(updatedEmployee.LastName) ? existingEmployee.LastName : updatedEmployee.LastName;
            existingEmployee.Address = string.IsNullOrWhiteSpace(updatedEmployee.Address) ? existingEmployee.Address : updatedEmployee.Address;
            existingEmployee.City = string.IsNullOrWhiteSpace(updatedEmployee.City) ? existingEmployee.City : updatedEmployee.City;

            // BirthDate için özel kontrol
            if (updatedEmployee.BirthDate.Year >= 1900)
            {
                existingEmployee.BirthDate = updatedEmployee.BirthDate;
            }
            else
            {
                return BadRequest(ModelState);
            }

            if (updatedEmployee.AddDate.Year >= 1900)
            {
                existingEmployee.AddDate = updatedEmployee.AddDate;
            }
            else
            {
                return BadRequest(ModelState);
            }


            // Eğer istemci tarafından gönderilen verilerle bir hata oluşursa
            if (!TryValidateModel(existingEmployee))
            {
                return BadRequest(ModelState);
            }

            _context.SaveChanges();

            return Ok(existingEmployee);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //disaridan gelen id ile blogu bul
            var employee = _context.Employees.FirstOrDefault(x => x.Id == id);

            //blog yoksa 404 döndür
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return Ok(employee);
        }
    }
}
