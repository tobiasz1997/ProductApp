using ProductApp.Application.Common.Abstraction;
using ProductApp.Application.User.DTO;

namespace ProductApp.Application.User.Commands;

public record RefreshToken(string Token) : ICommand<AuthResultDto>;