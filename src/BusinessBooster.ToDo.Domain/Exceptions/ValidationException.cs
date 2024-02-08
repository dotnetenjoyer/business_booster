namespace BusinessBooster.ToDo.Domain.Exceptions;

/// <summary>
/// The validation exception, can be mapped to 400 response.
/// </summary>
public class ValidationException : DomainException
{
    private readonly ValidationErrorFormatter.MessageFormatter messageMessageFormatter =
        ValidationErrorFormatter.SummeryOrDefaultMessageFormatter;
    
    /// <summary>
    /// Errors dictionary. Key is a member name, value is enumerable of error messages.
    /// Empty member name relates to a summery error messages.
    /// </summary>
    public ValidationErrors Errors { get; } = new();

    /// <inheritdoc />
    public override string Message => messageMessageFormatter(base.Message, Errors);
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public ValidationException() : base("An validation exception has occurred.")
    {
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public ValidationException(string message) : base(message)
    {
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="errors">Validation errors.</param>
    public ValidationException(IDictionary<string, ICollection<string>> errors)
    {
        if (errors == null)
        {
            throw new ArgumentNullException(nameof(errors));
        }

        Errors.Merge(errors);
    }
}