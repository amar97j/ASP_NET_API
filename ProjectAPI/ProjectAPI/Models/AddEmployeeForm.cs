using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class AddEmployeeForm
    {
       

        [Required]
        public string Name { get; set; }

        [Required]
        public string CivilId { get; set; }

        [Required]
        public string Position { get; set; }

        
    }
}
