using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Application.Responses;
using ft.user_management.Application.Dtos.User;
using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Application.Features.Users.Requests.Queries;

namespace ft.user_management.Application.Features.Users.Handlers.Queries;

public class GetUserByEmailRequestHandler :IRequestHandler<GetUserByEmailRequest, ReadResourceRequestResponse<ReadUserDto>>
{
    private readonly IUsersService _usersService;
    private readonly IMapper _mapper;
    
    public GetUserByEmailRequestHandler(IUsersService usersService, IMapper mapper)
    {
        _usersService = usersService;
        _mapper = mapper;
    }
    
    public async Task<ReadResourceRequestResponse<ReadUserDto>> Handle(GetUserByEmailRequest request, CancellationToken cancellationToken)
    {
        var response = new ReadResourceRequestResponse<ReadUserDto>();
        if (string.IsNullOrEmpty(request.Email))
        {
            response.Success = false;
            response.Message = "Email is required.";
            return response;
        }

        var user = await _usersService.GetUserByEmailAsync(request.Email);
        if (user == null)
        {
            response.Success = false;
            response.Message = "User not found.";
            return response;
        }

        response.Success = true;
        response.Message = $"User with email {request.Email} found.";
        response.Resource = _mapper.Map<ReadUserDto>(user);
        return response;
    }
}