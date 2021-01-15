namespace EntityFramework_Demo.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Salary { get; set; }

        //public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}