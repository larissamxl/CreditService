using CreditService.Models;

namespace CreditService.Data
{
    public interface ICreditRepo
    {
        bool SaveChanges();

        // Customer
        void CreateCustomer(Customer customer);
        Customer GetCustomer(int custumerId);
        bool CustomerExists(int custumerId);

        // Credit
        Credit GetCredit(int custumerId);
        void CreateCredit(int customerId, int score, Credit credit);
        bool IsCreditApproved(int score);
    }
}
