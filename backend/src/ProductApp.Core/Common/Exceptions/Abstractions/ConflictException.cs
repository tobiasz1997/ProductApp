namespace ProductApp.Core.Common.Exceptions.Abstractions;

public abstract class ConflictException(string message) : Exception(message);