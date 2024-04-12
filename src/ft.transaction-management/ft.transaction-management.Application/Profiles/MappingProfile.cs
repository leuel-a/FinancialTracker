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
        CreateMap<Transaction, CreateTransactionDto>().ReverseMap();
        CreateMap<Transaction, UpdateTransactionDto>().ReverseMap();
        CreateMap<Transaction, DeleteTransactionDto>().ReverseMap(); // TODO: This might be an necessary mapping

        #endregion
    }
}