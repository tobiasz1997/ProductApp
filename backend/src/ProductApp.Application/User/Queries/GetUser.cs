using ProductApp.Application.Common.Abstraction;
using ProductApp.Application.User.DTO;

namespace ProductApp.Application.User.Queries;

public class GetUser : IQuery<UserDto>
{
    public Guid UserId { get; set; }
}