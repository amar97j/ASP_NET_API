using WebApplication2.Models;

namespace ProjectAPI.Models
{
    public class AddEmployeeRequest
    {
        public int Id { get; internal set; }
        public string Name { get; set;}
        public string CivilId { get; set;}
        public string Position { get; set;}

        public BankBranch BankBranch { get; set;}
    }

}