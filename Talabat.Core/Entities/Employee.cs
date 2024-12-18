namespace Talabat.Core.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        List<Employee> Employees = new List<Employee>()
        {
            new Employee(){Id = 1 , Name = "Ahmed" , Age = 50},
            new Employee(){Id = 2 , Name = "Salah" , Age = 35},
            new Employee(){Id = 3 , Name = "Hamed" , Age = 20},
            new Employee(){Id = 4 , Name = "Mo" , Age = 30}
        };

    }
}
