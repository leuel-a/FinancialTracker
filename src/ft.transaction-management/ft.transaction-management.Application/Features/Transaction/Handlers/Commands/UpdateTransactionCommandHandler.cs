using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ft.transaction_management.Application.Responses;
using ft.transaction_management.Application.Contracts.Persistence;
using ft.transaction_management.Application.Dtos.Transaction.Validators;
using ft.transaction_management.Application.Features.Transaction.Requests.Commands;

namespace ft.transaction_management.Application.Features.Transaction.Handlers.Commands;

public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, BaseCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly ITransactionsRepository _transactionsRepository;

    public UpdateTransactionCommandHandler(ITransactionsRepository transactionsRepository, IMapper mapper,
        ICategoriesRepository categoriesRepository)
    {
        _mapper = mapper;
        _categoriesRepository = categoriesRepository;
        _transactionsRepository = transactionsRepository;
    }

    public async Task<BaseCommandResponse> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new UpdateTransactionDtoValidator(_transactionsRepository, _categoriesRepository);
        var validationResult = await validator.ValidateAsync(request.TransactionDto!, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Succeeded = false;
            response.Message = "Validation errors have occured.";
            response.ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return response;
        }

        var transaction = await _transactionsRepository.GetByIdAsync(request.TransactionDto!.Id);
        _mapper.Map(request.TransactionDto, transaction);

        // Update the transaction in the database
        var result = await _transactionsRepository.UpdateAsync(transaction);

        if (result.Succeeded == false)
        {
            response.Succeeded = false;
            response.Message = result.Message;
        }
        else
        {
            response.Succeeded = true;
            response.Message = "Transaction successfully updated.";
        }

        return response;
    }
}