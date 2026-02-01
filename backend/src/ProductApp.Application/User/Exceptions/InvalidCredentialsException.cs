using ProductApp.Core.Common.Exceptions.Abstractions;

namespace ProductApp.Application.User.Exceptions;

public sealed class InvalidCredentialsException() : BadRequestException("Invalid credentials");