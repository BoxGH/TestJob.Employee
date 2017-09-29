using System.ComponentModel.DataAnnotations;

namespace TestJob.Employee.ViewModel
{
    public class Worker
    {
        [RegularExpression(@"^[A-ZА-Я][a-zа-я]{1,31}", ErrorMessage = "Enter something similar - Jon")]
        public string FirstName { get; set; }
        [RegularExpression(@"^[A-ZА-Я][a-zа-я]{1,31}", ErrorMessage = "Enter something similar - Dou")]
        public string LastName { get; set; }
        [RegularExpression(@"^[A-ZА-Я][a-zа-я].{2,256}", ErrorMessage = "Enter Profession name")]
        public string Position { get; set; }
        public bool Status { get; set; }
        [Range(100.00,1000000.00,ErrorMessage = "Enter the value of the salary 100.0 - 1000000.0")]
        public decimal Tasso { get; set; }
    }
}
