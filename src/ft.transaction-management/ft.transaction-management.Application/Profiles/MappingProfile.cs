using AutoMapper;
using ft.transaction_management.Application.DTOs.TransactionDto;
using ft.transaction_management.Domain.Entities;

namespace ft.transaction_management.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Transaction Dto Mappings

        CreateMap<Transaction, ReadTransactionDto>().ReverseMap();

        #endregion
    }
}