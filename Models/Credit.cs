using System.ComponentModel.DataAnnotations;

namespace CreditService.Models
{
    public class Credit
    {
        public Credit()
        {
            isCreditApproved = false;
            Value = 0;
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int IdCustomer { get; set; }

        [Required]
        public bool isCreditApproved { get; set; }

        [Required]
        public int Value { get; set; }

        public Customer Customer { get; set; }
    }
}
