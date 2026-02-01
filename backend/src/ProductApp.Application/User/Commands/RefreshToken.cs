using ProductApp.Application.Common.Abstraction;

namespace ProductApp.Application.User.Commands;

public record RefreshToken(string Token) : ICommand;