using System.ComponentModel.DataAnnotations;

namespace CreditService.Dtos
{
    public class CreditCreateDto
    {
        [Required]
        public bool isCreditApproved { get; set; }

        [Required]
        public int Value { get; set; }
    }
}
