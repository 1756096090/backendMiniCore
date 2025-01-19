using backendMiniCore.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backendMiniCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class DepartmentController : ControllerBase
    {
        private readonly IMongoCollection<Department> _departmentCollection;
        public DepartmentController(IMongoDatabase database)
        {
            _departmentCollection = database.GetCollection<Department>("Department");
        }
        // GET: api/<Department>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> Get()
        {
            return await _departmentCollection.Find(_ => true).ToListAsync();
        }

        // GET api/<Department>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> Get(string id)
        {
            var department = await _departmentCollection.Find(d => d.DepartmentId == id).FirstOrDefaultAsync();
            if (department == null)
            {
                return NotFound();
            }

            return department;

        }

        // POST api/<Department>
        [HttpPost]
        public async Task<ActionResult<Department>> Post(Department department)
        {
            await _departmentCollection.InsertOneAsync(department);
            return CreatedAtAction(nameof(Get), new { id = department.DepartmentId });

        }

        // PUT api/<Department>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Department>> Put(string id, Department department)
        {
            var result = await _departmentCollection.ReplaceOneAsync(d => d.DepartmentId == id , department);
            if(result.ModifiedCount == 0)
            {
                return NotFound();
            }
            return NoContent();

        }

        //// DELETE api/<Department>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
