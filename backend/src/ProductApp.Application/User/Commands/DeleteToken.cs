using ProductApp.Application.Common.Abstraction;

namespace ProductApp.Application.User.Commands;

public record DeleteToken(string Token) : ICommand;
