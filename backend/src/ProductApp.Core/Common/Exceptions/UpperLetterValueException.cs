using ProductApp.Core.Common.Exceptions.Abstractions;

namespace ProductApp.Core.Common.Exceptions;

public sealed class UpperLetterValueException(string name) : BadRequestException($"{name} must contain uppercase letter.");