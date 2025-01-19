using backendMiniCore.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backendMiniCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMongoCollection<Employee> _employeeCollection;
        public EmployeeController(IMongoDatabase database)
        {
            _employeeCollection = database.GetCollection<Employee>("Employees");
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Get()
        {
            return await _employeeCollection.Find(_ => true).ToListAsync();
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(string id)
        {
            var employee = await _employeeCollection.Find(e => e.EmployeeId == id).FirstOrDefaultAsync();
            if (employee == null) { 
                return NotFound();
            }

            return employee;
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<ActionResult<Employee>> Post(Employee employee)
        {
            await _employeeCollection.InsertOneAsync(employee);
            return CreatedAtAction(nameof(Get), new { id = employee.EmployeeId });

        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, Employee employee)
        {
            var result = await _employeeCollection.ReplaceOneAsync(e => e.EmployeeId == id, employee);
            if (result.ModifiedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE api/<EmployeeController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
