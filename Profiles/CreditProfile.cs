using AutoMapper;
using CreditService.Dtos;
using CreditService.Models;

namespace CreditService.Profiles
{
    public class CreditProfile : Profile
    {
        public CreditProfile()
        {
            CreateMap<Customer, CustomerReadDto>();
            CreateMap<CreditCreateDto, Credit>();
            CreateMap<Credit, CreditReadDto>();
            CreateMap<CustomerPublishedDto, Customer>()
                .ForMember(dest => dest.IdCustomer, opt => opt.MapFrom(src => src.Id));
        }
    }
}
