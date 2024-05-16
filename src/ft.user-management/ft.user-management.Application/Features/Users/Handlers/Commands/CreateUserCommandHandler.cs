using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Domain.Entities;
using ft.user_management.Application.Responses;
using ft.user_management.Application.Dtos.User;
using ft.user_management.Application.Dtos.User.Validators;
using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Application.Features.Users.Requests.Commands;

namespace ft.user_management.Application.Features.Users.Handlers.Commands;

public class CreateUserCommandHandler: IRequestHandler<CreateUserCommand, ReadResourceResponse<ReadUserDto>>
{
    private readonly IMapper _mapper;
    private readonly IUsersService _usersService;

    public CreateUserCommandHandler(IUsersService usersService, IMapper mapper)
    {
        _mapper = mapper;
        _usersService = usersService;
    }
    public async Task<ReadResourceResponse<ReadUserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var response = new ReadResourceResponse<ReadUserDto>(); 
        var validator = new CreateUserDtoValidator();
        var validationResult = await validator.ValidateAsync(request.UserDto, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Validation errors occured.";
            response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return response;
        }
        
        var user = _mapper.Map<ApplicationUser>(request.UserDto);
        await _usersService.AddAsync(user, request.UserDto.Password!);

        response.Success = true;
        response.Message = "User created successfully.";
        response.Id = user.Id;
        response.Resource = _mapper.Map<ReadUserDto>(user);
        return response;
    }
}