using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ft.transaction_management.Application.Responses;
using ft.transaction_management.Application.Dtos.Transaction;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.Dtos.Transaction.Validators;
using ft.transaction_management.Application.Features.Transaction.Requests.Commands;

namespace ft.transaction_management.Application.Features.Transaction.Handlers.Commands;

public class
    CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand,
    CreateCommandResponse<ReadTransactionDto>>
{
    private readonly IMapper _mapper;
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly ITransactionsRepository _transactionsRepository;

    public CreateTransactionCommandHandler(IMapper mapper, ITransactionsRepository transactionsRepository, ICategoriesRepository categoriesRepository)
    {
        _mapper = mapper;
        _categoriesRepository = categoriesRepository;
        _transactionsRepository = transactionsRepository;
    }

    public async Task<CreateCommandResponse<ReadTransactionDto>> Handle(CreateTransactionCommand request,
        CancellationToken cancellationToken)
    {
        var response = new CreateCommandResponse<ReadTransactionDto>();
        var validator = new CreateTransactionDtoValidator(_categoriesRepository);
        var validationResult = await validator.ValidateAsync(request.CreateTransactionDto!, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Succeeded = false;
            response.Message = "Validation error has occured";
            response.ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return response;
        }

        var transaction = _mapper.Map<Domain.Entities.Transaction>(request.CreateTransactionDto);
        
        // Add the transaction to the database
        var result = await _transactionsRepository.AddAsync(transaction);

        if (result.Succeeded == false)
        {
            response.Succeeded = false;
            response.Message = result.Message;
        }
        else
        {
            response.Succeeded = true;
            response.Id = transaction.Id;
            response.Message = "Transaction successfully created.";
            response.CreatedResource = _mapper.Map<ReadTransactionDto>(transaction);
        }
        return response;
    }
}