using System.ComponentModel.DataAnnotations;

namespace CreditService.Models
{
    public class Customer
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int IdCustomer { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Score { get; set; }

        public Credit Credit { get; set; }
    }
}
