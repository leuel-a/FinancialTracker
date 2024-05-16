using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ft.user_management.Application.Responses;
using ft.user_management.Application.Dtos.User;
using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Application.Features.Users.Requests.Queries;

namespace ft.user_management.Application.Features.Users.Handlers.Queries;

public class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequest, ReadResourceResponse<ReadUserDto>>
{
    private readonly IMapper _mapper;
    private readonly IUsersService _usersService;

    public GetAllUsersRequestHandler(IUsersService usersService, IMapper mapper)
    {
        _mapper = mapper;
        _usersService = usersService;
    }

    public async Task<ReadResourceResponse<ReadUserDto>> Handle(GetAllUsersRequest request,
        CancellationToken cancellationToken)
    {
        var response = new ReadResourceResponse<ReadUserDto>();
        var users = await _usersService.GetAllAsync();

        response.Success = true;
        response.Message = "Users have been successfully retrieved";
        response.Resources = _mapper.Map<List<ReadUserDto>>(users);
        
        return response;
    }
}