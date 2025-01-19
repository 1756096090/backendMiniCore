namespace backendMiniCore.Config
{
    public class MongoDBSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName {  get; set; } = string.Empty ;
        public string EmployeesCollection {  get; set; } = string.Empty ;
        public string DepartmentCollection {  get; set; } = string.Empty ;
        public string ExpensesCollection { get; set; } = string.Empty;

    }
}
