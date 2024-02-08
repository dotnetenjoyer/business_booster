namespace BusinessBooster.ToDo.Domain.Exceptions;

/// <summary>
/// Contains methods to format validation error messages.
/// </summary>
public class ValidationErrorFormatter
{
    /// <summary>
    /// Message formatter delegate that is used to formatting error validation message.
    /// </summary>
    public delegate string MessageFormatter(string defaultMessage, ValidationErrors validationErrors);
    
    /// <summary>
    /// Returns summery message if a specific key exists or defaults message.
    /// </summary>
    /// <param name="defaultMessage">Default message. </param>
    /// <param name="validationErrors">Validation errors.</param>
    public static string? SummeryOrDefaultMessageFormatter(string defaultMessage, ValidationErrors validationErrors)
    {
        if (validationErrors == null)
        {
            throw new ArgumentNullException(nameof(validationErrors));
        }

        if (validationErrors.ContainsKey(ValidationErrors.SummeryKey))
        {
            return validationErrors.SummeryErrors.First();
        }

        return defaultMessage;
    }
}