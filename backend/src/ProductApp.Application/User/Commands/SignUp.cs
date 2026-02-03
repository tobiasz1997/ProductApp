using ProductApp.Application.Common.Abstraction;

namespace ProductApp.Application.User.Commands;

public record SignUp(string Login, string Password) : ICommand;