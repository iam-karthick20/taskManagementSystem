using System;

namespace TMS.Application.Exceptions;

public class DuplicateUserException(string username) : Exception($"User with username '{username}' already exists.")
{
}

public class DuplicateUserEmailException(string email) : Exception($"User with EmailAddress '{email}' already exists.")
{
}

public class UpdateUserNotFoundException(string userId) : Exception($"User with User ID '{userId}' not found.")
{
}

public class UsersGetNotFoundException() : Exception($"Users not found in the Database")
{
}

public class UserGetNotFoundException(Guid userId) : Exception($"User: {userId} not found in the Database")
{
}

public class TaskNotFoundException(string Id) : Exception($"Task with ID '{Id}' not found.")
{
}

public class TaskUserNotMatchedException(string Id, string OwnerUserId) : Exception($"Task with ID: '{Id}' is not access by User: {OwnerUserId}.")
{
}

public class TaskGetNotFoundException() : Exception($"Task not found in the Database")
{
}