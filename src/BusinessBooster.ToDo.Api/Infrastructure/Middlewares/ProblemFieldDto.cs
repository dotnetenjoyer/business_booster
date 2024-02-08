namespace BusinessBooster.ToDo.Api.Infrastructure.Middlewares;

/// <summary>
/// Describe filed validation problems.
/// </summary>
public class ProblemFieldDto
{
    /// <summary>
    /// Field name.
    /// </summary>
    public string Field { get; }
    
    /// <summary>
    /// Error messages.
    /// </summary>
    public IReadOnlyList<string> Messages { get;  }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="field">Field name.</param>
    /// <param name="messages">Error messages.</param>
    public ProblemFieldDto(string field, IEnumerable<string> messages)
    {
        if (messages == null)
        {
            throw new ArgumentNullException(nameof(messages));
        }
        
        Field = field;
        Messages = messages.ToList();
    }
}