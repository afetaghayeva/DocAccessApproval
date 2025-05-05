namespace DocAccessApproval.Domain.Exceptions.UserExceptions;

public class UserException:Exception
{
    public UserException() { }

    public UserException(string exception) : base(exception) { }

    public UserException(string exception, Exception innerException) : base(exception, innerException) { }

    public static void ThrowIfNull(object? obj)
    {
        if (obj == null)
            throw new UserException($"{nameof(obj)} cannot be empty");
    }

    public static void ThrowIfNullOrEmpty(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new UserException($"{nameof(value)} cannot be empty");
    }

    public static void ThrowIfDefault(object? obj)
    {
        if (obj == default)
            throw new UserException($"{nameof(obj)} cannot be empty");
    }
}
