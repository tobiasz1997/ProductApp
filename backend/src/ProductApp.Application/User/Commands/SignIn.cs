using ProductApp.Application.Common.Abstraction;

namespace ProductApp.Application.User.Commands;

public record SignIn(string Login, string Password) : ICommand;