using ProductApp.Application.Common.Abstraction;

namespace ProductApp.Application.User.Commands;

public record SignInOrCreate(string Login, string Password) : ICommand;