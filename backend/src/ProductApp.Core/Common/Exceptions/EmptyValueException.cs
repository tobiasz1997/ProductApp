using ProductApp.Core.Common.Exceptions.Abstractions;

namespace ProductApp.Core.Common.Exceptions;

public sealed class EmptyValueException(string name) : BadRequestException($"{name} is empty.");