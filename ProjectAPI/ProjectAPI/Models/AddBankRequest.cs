namespace ProjectAPI.Models
{
    public class AddBankRequest
    {
        public string LocationName { get; set; }
        public string LocationURL { get; set; }
        public string BranchManager { get; set; }
        public int EmployeeCount { get; set; }
    }

  

    public class BankBranchResponce
    {
        public string LocationName { get; set; }
        public string LocationURL { get; set; }
        public string BranchManager { get; set; }
        public int EmployeeCount { get; set; }
    }
}