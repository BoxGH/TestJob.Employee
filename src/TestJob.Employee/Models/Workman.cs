using System.ComponentModel.DataAnnotations;

namespace TestJob.Employee.Models
{
    public class Workman
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(32)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(32)]
        public string LastName { get; set; }
        [Required]
        [StringLength(256)]
        public string Position { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public decimal Tasso { get; set; }
        [Required]
        public int TaxRate { get; set; }
        [Required]
        public decimal Tax { get; set; }
        [Required]
        public decimal Wages { get; set; }
    }
}
