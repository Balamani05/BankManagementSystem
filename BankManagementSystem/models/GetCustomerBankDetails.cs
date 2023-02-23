namespace BankManagementSystem.models
{
    public class GetCustomerBankDetails
    {
        public string? AccountHolder { get; set; }
        public long AccountNumber { get; set; }
        public string? IFSCCode { get; set; }
        public string? BankName { get; set; }
        public string? Branch { get; set; }
      //  public decimal CurrentBalance { get; set; }
    }
}
