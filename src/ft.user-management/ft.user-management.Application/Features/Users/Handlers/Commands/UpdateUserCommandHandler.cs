using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Application.Dtos.User.Validators;
using ft.user_management.Application.Features.Users.Requests.Commands;
using ft.user_management.Application.Responses;
using MediatR;

namespace ft.user_management.Application.Features.Users.Handlers.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseResponse>
{
    private readonly IMapper _mapper;
    private readonly IUsersService _usersService;

    public UpdateUserCommandHandler(IUsersService usersServices, IMapper mapper)
    {
        _mapper = mapper;
        _usersService = usersServices;
    }

    public async Task<BaseResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();
        request.UserDto.Id = request.Id;
        
        var validator = new UpdateUserDtoValidator(_usersService);
        var validationResult = await validator.ValidateAsync(request.UserDto, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "User update unsuccessful. Refer the errors for more information.";
            response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

            return response;
        }

        var user = await _usersService.GetByIdAsync(request.UserDto.Id);
        _mapper.Map(request.UserDto, user);

        var result = await _usersService.UpdateAsync(user!);
        if (result.Succeeded == false)
        {
            response.Success = false;
            response.Message = "User update unsuccessful. Refer the errors for more information.";
            response.Errors = result.Errors.Select(e => e.Description).ToList();
            
            return response;
        }

        response.Success = true;
        response.Message = "User update successful.";
        response.Id = user!.Id;

        return response;
    }
}