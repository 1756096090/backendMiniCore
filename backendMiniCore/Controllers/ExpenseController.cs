using backendMiniCore.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backendMiniCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IMongoCollection<Expense> _expenseController;
        private readonly IMongoCollection<Employee> _employeeController;
        private readonly IMongoCollection<Department> _departmentController;
        
        public ExpenseController(IMongoDatabase database)
        {
            _expenseController = database.GetCollection<Expense>("Expense");
            _employeeController = database.GetCollection<Employee>("Employee");
            _departmentController = database.GetCollection<Department>("Department");

        }
        // GET: api/<ExpenseController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> Get()
        {
            return await _expenseController.Find(_ => true).ToListAsync();
            
        }

        // GET api/<ExpenseController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> Get(string id)
        {
            var expense = await _expenseController.Find(e => e.ExpenseID == id).FirstOrDefaultAsync();
            if (expense == null) {
                return NotFound();
            }
            return expense;
        }

        // POST api/<ExpenseController>
        [HttpPost]
        public async Task<ActionResult<Expense>> Post(Expense expense)
        {
            await _expenseController.InsertOneAsync(expense);
            return CreatedAtAction(nameof(Get), new { id = expense.ExpenseID });
        }

        // PUT api/<ExpenseController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Expense>> Put(string id, Expense expense)
        {
            var result = await _expenseController.ReplaceOneAsync(e => e.ExpenseID ==id, expense);
            if (result.ModifiedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }


        public class DepartmentAmount
        {
            public string DepartmentName { get; set; }
            public decimal TotalAmount { get; set; }
        }

        [HttpGet("filter_by_dates/{firstDate}/{endDate}")]
        public async Task<ActionResult<IEnumerable<object>>> FilterByDates(DateTime firstDate, DateTime endDate)
        {
            try
            {
                if (firstDate > endDate)
                {
                    return BadRequest("Start date must be before end date");
                }

                var filterBuilder = Builders<Expense>.Filter;
                var filter = filterBuilder.Gte(e => e.Date, firstDate) &
                             filterBuilder.Lte(e => e.Date, endDate);

                // Fetch filtered expenses
                var expenses = await _expenseController
                    .Find(filter)
                    .Sort(Builders<Expense>.Sort.Ascending(e => e.Date))
                    .ToListAsync();

                if (!expenses.Any())
                {
                    return NotFound("No expenses found for the given date range.");
                }

                var employeeIDs = expenses.Select(e => e.EmployeeId).Distinct().ToList();
                var departmentIDs = expenses.Select(e => e.DepartmentId).Distinct().ToList();

                var employees = await _employeeController
                    .Find(Builders<Employee>.Filter.In(e => e.EmployeeId, employeeIDs))
                    .ToListAsync();

                var departments = await _departmentController
                    .Find(Builders<Department>.Filter.In(d => d.DepartmentId, departmentIDs))
                    .ToListAsync();

                var result = expenses.Select(expense => new
                {
                    Expense = expense,
                    Employee = employees.FirstOrDefault(e => e.EmployeeId == expense.EmployeeId),
                    Department = departments.FirstOrDefault(d => d.DepartmentId == expense.DepartmentId)
                });

                var amountByDepartment = new Dictionary<string, DepartmentAmount >();

                foreach(var item in result)
                {
                    var department = item.Department;
                    var expense = item.Expense;

                    if(department != null)
                    {
                        if (amountByDepartment.ContainsKey(department.DepartmentId)){

                            amountByDepartment[department.DepartmentId].TotalAmount += expense.Amount;
                        }
                        else
                        {
                            amountByDepartment.Add(department.DepartmentId, new DepartmentAmount
                            {
                                DepartmentName = department.Name, 
                                TotalAmount = expense.Amount
                            });


                        }
                    } 
                }

               

                return Ok(amountByDepartment);
            }
            catch (Exception ex)
            {
                // Log the exception (if logging is configured)
                Console.Error.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving expenses.");
            }
        }



        //// DELETE api/<ExpenseController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
