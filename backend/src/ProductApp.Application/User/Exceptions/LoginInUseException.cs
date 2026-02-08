using ProductApp.Core.Common.Exceptions.Abstractions;

namespace ProductApp.Application.User.Exceptions;

public sealed class LoginInUseException() : ConflictException("Login in use.");