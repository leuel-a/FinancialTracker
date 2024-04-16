using AutoMapper;
using ft.transaction_management.Domain.Entities;
using ft.transaction_management.Application.Dtos.CategoryDto;
using ft.transaction_management.Application.DTOs.CategoryDto;
using ft.transaction_management.Application.DTOs.TransactionDto;

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

        #region Categories Dto Mappings

        CreateMap<Category, ReadCategoryDto>().ReverseMap();
        CreateMap<Category, ReadCategoryWithTransactionsDto>()
            .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions));
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, DeleteCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();

        #endregion
    }
}