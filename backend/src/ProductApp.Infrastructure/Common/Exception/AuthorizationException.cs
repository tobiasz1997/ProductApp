using ProductApp.Core.Common.Exceptions.Abstractions;

namespace ProductApp.Infrastructure.Common.Exception;

public sealed class AuthorizationException() : UnauthorizedException("Unauthorized");