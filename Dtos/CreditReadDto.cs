namespace CreditService.Dtos
{
    public class CreditReadDto
    {
        public int Id { get; set; }
        public int IdCustomer { get; set; }
        public bool isCreditApproved { get; set; }
        public int Value { get; set; }
    }
}
