namespace DocAccessApproval.Domain.Exceptions.DocumentExceptions;

public class DocumentException:Exception
{
    public DocumentException() { }

    public DocumentException(string exception) : base(exception) { }

    public DocumentException(string exception, Exception innerException) : base(exception, innerException) { }

    public static void ThrowIfNull(object? obj)
    {
        if (obj == null)
            throw new DocumentException($"{nameof(obj)} cannot be empty");
    }

    public static void ThrowIfNullOrEmpty(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new DocumentException($"{nameof(value)} cannot be empty");
    }

    public static void ThrowIfDefault(object? obj)
    {
        if (obj == default)
            throw new DocumentException($"{nameof(obj)} cannot be empty");
    }
}
