using ProductApp.Core.Common.Exceptions.Abstractions;

namespace ProductApp.Core.Common.Exceptions;

public sealed class FormatException(string name) : BadRequestException($"Invlaid {name} format.");