namespace ProductApp.Core.Common.Exceptions.Abstractions;

public abstract class NotFoundException(string message) : Exception(message);