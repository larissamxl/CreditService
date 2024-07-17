using AutoMapper;
using CreditService.Data;
using CreditService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CreditService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICreditRepo _repository;
        private readonly IMapper _mapper;

        public CustomerController(ICreditRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{customerId}")]
        public ActionResult<CustomerReadDto> GetCustomer(int customerId)
        {
            Console.WriteLine("---> Buscando Clientes");

            if (!_repository.CustomerExists(customerId))
            {
                return NotFound();
            }

            var customers = _repository.GetCustomer(customerId);

            return Ok(_mapper.Map<CustomerReadDto>(customers));
        }
    }
}
