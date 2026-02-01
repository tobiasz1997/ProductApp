using ProductApp.Core.Common.Exceptions.Abstractions;

namespace ProductApp.Application.User.Exceptions;

public sealed class UserNotFoundException(Guid id) : NotFoundException($"User with id = {id} is not exist.");