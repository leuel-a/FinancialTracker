using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Application.Responses;
using ft.user_management.Application.Dtos.User;
using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Application.Features.Users.Requests.Queries;

namespace ft.user_management.Application.Features.Users.Handlers.Queries;

public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, ReadResourceResponse<ReadUserDto>>
{
    private readonly IUsersService _usersService;
    private readonly IMapper _mapper;

    public GetUserByIdRequestHandler(IUsersService usersService, IMapper mapper)
    {
        _mapper = mapper;
        _usersService = usersService;
    }

    public async Task<ReadResourceResponse<ReadUserDto>> Handle(GetUserByIdRequest request,
        CancellationToken cancellationToken)
    {
        var response = new ReadResourceResponse<ReadUserDto>();
        var user = await _usersService.GetByIdAsync(request.Id);

        if (user == null)
        {
            response.Success = false;
            response.Message = "User not found";

            return response;
        }

        response.Success = true;
        response.Message = "User found";
        response.Resource = _mapper.Map<ReadUserDto>(user);

        return response;
    }
}