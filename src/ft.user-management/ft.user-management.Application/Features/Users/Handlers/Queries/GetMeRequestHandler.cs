using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using ft.user_management.Application.Dtos.User;
using ft.user_management.Application.Responses;
using ft.user_management.Application.Contracts.Infrastructure;
using ft.user_management.Application.Features.Users.Requests.Queries;

namespace ft.user_management.Application.Features.Users.Handlers.Queries;

public class GetMeRequestHandler: IRequestHandler<GetMeRequest, ReadResourceResponse<ReadUserDto>>
{
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IUsersService _usersService;

    public GetMeRequestHandler(ITokenService tokenService, IUsersService usersService, IMapper mapper)
    {
        _tokenService = tokenService;
        _usersService = usersService;
        _mapper = mapper;
    }
    
    public async Task<ReadResourceResponse<ReadUserDto>> Handle(GetMeRequest request, CancellationToken cancellationToken)
    {
        var response = new ReadResourceResponse<ReadUserDto>();
        var id = _tokenService.GetUserIdFromToken(request.AccessToken!);

        if (id == null)
        {
            response.Success = false;
            response.Message = "Invalid token";

            return response;
        }

        var user = await _usersService.GetByIdAsync(id.Value);
        if (user == null)
        {
            response.Success = false;
            response.Message = "User can not be found";

            return response;
        }

        response.Success = true;
        response.Message = "Successfully got user.";
        response.Resource = _mapper.Map<ReadUserDto>(user);

        return response;
    }
}