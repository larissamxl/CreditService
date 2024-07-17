using AutoMapper;
using CreditService.Data;
using CreditService.Dtos;
using CreditService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CreditService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly ICreditRepo _repository;
        private readonly IMapper _mapper;

        public CreditController(ICreditRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{customerId}", Name = "GetCreditForCustomer")]
        public ActionResult<CreditReadDto> GetCreditForCustomer(int customerId)
        {
            Console.WriteLine($"--> Hit GetCreditForCustomer: {customerId}");

            if (!_repository.CustomerExists(customerId))
            {
                return NotFound();
            }

            var command = _repository.GetCredit(customerId);

            if (command == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CreditReadDto>(command));
        }

        [HttpPost]
        public ActionResult<CreditReadDto> CreateCredit(int customerId,int score)
        {
            Console.WriteLine($"--> Hit CreateCredit: {customerId}");

            if (!_repository.CustomerExists(customerId))
            {
                return NotFound();
            }

            CreditCreateDto creditDto = new CreditCreateDto();
            var credit = _mapper.Map<Credit>(creditDto);
            credit.IdCustomer = customerId;

            var isCreditApproved = _repository.IsCreditApproved(score);

            if (isCreditApproved)
            {
                credit.isCreditApproved = true;
                _repository.CreateCredit(customerId, score, credit);
            }

            _repository.SaveChanges();
            var creditReadDto = _mapper.Map<CreditReadDto>(credit);

            return Ok(creditReadDto);
        }
    }
}
