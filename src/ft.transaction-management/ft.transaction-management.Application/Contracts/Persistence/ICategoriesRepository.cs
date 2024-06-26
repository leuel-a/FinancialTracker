using System.Threading.Tasks;
using ft.transaction_management.Application.Models;
using ft.transaction_management.Domain.Entities;

namespace ft.transaction_management.Application.Contracts.Persistence;

public interface ICategoriesRepository: IGenericRepository<Category>
{
}