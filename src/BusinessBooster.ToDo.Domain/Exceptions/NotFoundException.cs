namespace BusinessBooster.ToDo.Domain.Exceptions;

/// <summary>
/// The not found exception. Can be mapped to 404 response.
/// </summary>
public class NotFoundException : DomainException
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public NotFoundException(string message) : base(message)
    {
    }
}