using ProductApp.Application.Common.Abstraction;
using ProductApp.Application.User.DTO;
using ProductApp.Application.User.Exceptions;
using ProductApp.Core.Users.Repositories;

namespace ProductApp.Application.User.Queries.Handlers;

public class GetUserHandler :IQueryHandler<GetUser, UserDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> HandleAsync(GetUser query)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId);

        if (user is null)
        {
            throw new UserNotFoundException(query.UserId);
        }

        return new UserDto()
        {
            Id = user.Id,
            Login = user.Login,
        };
    }
}