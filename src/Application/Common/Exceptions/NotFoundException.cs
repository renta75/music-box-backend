namespace CleanArchitecture.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException()
        : base()
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
}

public class NotASingleEntityException : Exception
{
    public NotASingleEntityException()
        : base()
    {
    }

    public NotASingleEntityException(string message)
        : base(message)
    {
    }

    public NotASingleEntityException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public NotASingleEntityException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
}
