using CreditService.Models;

namespace CreditService.Data
{
    public class CreditRepo : ICreditRepo
    {
        private readonly AppDbContext _context;

        public CreditRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateCredit(int customerId, int score, Credit credit)
        {
            if (credit.isCreditApproved) 
            {                
                if (score < 100)
                    credit.Value = 1000;
                else if (score > 100 && score < 500)
                    credit.Value = 5000;
                else if (score > 500 && score < 800)
                    credit.Value = 5000;
                else if (score > 800)
                    credit.Value = 10000;

                _context.Credit.Add(credit);
            }
        }

        public bool CustomerExists(int custumerId)
        {
            return _context.Customer.Any(p => p.IdCustomer == custumerId);
        }

        public Credit GetCredit(int custumerId)
        {
            return _context.Credit.FirstOrDefault(p => p.IdCustomer == custumerId);
        }

        public Customer GetCustomer(int custumerId)
        {
            return _context.Customer.FirstOrDefault(p => p.IdCustomer == custumerId);
        }

        public void CreateCustomer(Customer customer) 
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            _context.Customer.Add(customer);
        }

        public bool IsCreditApproved(int score)
        {
            if (score > 100)
                return true;
            else
                return false;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
