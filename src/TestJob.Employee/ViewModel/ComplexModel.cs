using System.Collections.Generic;
using TestJob.Employee.Models;

namespace TestJob.Employee.ViewModel
{
    public class ComplexModel
    {
        public IEnumerable<Workman> Workmen { get; set; }
        public Worker Worker { get; set; }
        public int TotallPage { get; set; }
        public int CurrentPage { get; set; }
        public bool? Status { get; set; }
    }
}
