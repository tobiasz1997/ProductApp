namespace ProductApp.Core.Common.Exceptions.Abstractions;

public abstract class BadRequestException(string message) : Exception(message);