namespace WebApplication2.Models { 

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CivilId { get; set; }

        [Required]
        public string Position { get; set; }

        public BankBranch BankBranchEmployee { get; set; }
    }
}
