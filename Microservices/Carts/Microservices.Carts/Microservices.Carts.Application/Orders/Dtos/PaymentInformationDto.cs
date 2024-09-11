using AutoMapper;
using Microservices.Carts.Domain.ValueObjects;

namespace Microservices.Carts.Application.Dtos;

public class PaymentInformationDto
{
    public string? CardNumber { get; set; }
    public string? ExpireDate { get; set; }
    public string? Security { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<PaymentInformation, PaymentInformationDto>();
            CreateMap<PaymentInformationDto, PaymentInformation>();
        }
    }
}
