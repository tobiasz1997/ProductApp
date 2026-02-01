namespace ProductApp.Core.Common.Exceptions.Abstractions;

public abstract class UnauthorizedException(string message) : Exception(message);