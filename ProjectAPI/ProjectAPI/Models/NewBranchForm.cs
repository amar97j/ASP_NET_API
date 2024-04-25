using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class NewBranchForm
    {
        [Required]
        public string LocationName { get; set; }
        [Required]
        public string LocationURL { get; set; }
        [Required]
        public string BranchManager { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage = "Employee must be more than 0")]
        public int EmployeeCount { get; set; }
    }
}
