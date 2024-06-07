using AutoMapper;
using ft.transaction_management.Domain.Entities;
using ft.transaction_management.Application.Dtos.Category;
using ft.transaction_management.Application.Dtos.Transaction;

namespace ft.transaction_management.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Category Dto Mappings
        CreateMap<Category, ReadCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();

        // Transaction Dto Mappings
        CreateMap<Transaction,ReadTransactionDto>();
        CreateMap<ReadTransactionDto, Transaction>();

        CreateMap<Transaction, CreateTransactionDto>().ReverseMap();
        CreateMap<Transaction, UpdateTransactionDto>().ReverseMap();
    }
}