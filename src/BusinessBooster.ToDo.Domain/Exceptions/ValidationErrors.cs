using System.ComponentModel.DataAnnotations;

namespace BusinessBooster.ToDo.Domain.Exceptions;

/// <summary>
/// The collection of key-value validation messages. Allow accumulating errors per key.
/// </summary>
public class ValidationErrors : Dictionary<string, ICollection<string>>
{
    /// <summary>
    /// Default summery validation key. The error messages associated with this key are generic.
    /// </summary>
    public const string SummeryKey = "";

    /// <summary>
    /// Collection of summery errors.
    /// </summary>
    public IEnumerable<string> SummeryErrors => ContainsKey(SummeryKey) ? this[SummeryKey] : Array.Empty<string>();

    /// <summary>
    /// Add a generic error message.
    /// </summary>
    /// <param name="error">Error message.</param>
    public void AddError(string error)
    {
        AddError(SummeryKey, error);
    }
    
    /// <summary>
    /// Add a error to error list for the specific key.
    /// </summary>
    /// <param name="key">Error key.</param>
    /// <param name="error">Error message.</param>
    public void AddError(string key, string error)
    {
        AddErrors(key, new [] { error });
    }

    /// <summary>
    /// Add a errors to error list for the specific key.
    /// </summary>
    /// <param name="key">Error key.</param>
    /// <param name="errors">Errors messages</param>
    public void AddErrors(string key, IEnumerable<string> errors)
    {
        if (errors.Any(string.IsNullOrEmpty))
        {
            throw new ArgumentException("Please specify the error message", nameof(errors));
        }

        ICollection<string> internalErrors = TryGetValue(key, out internalErrors) 
            ? internalErrors 
            : new List<string>();

        if (!TryGetValue(key, out internalErrors))
        {
            internalErrors = new List<string>();
            Add(key, internalErrors); 
        }

        foreach (var error in errors)
        {
            internalErrors.Add(error);   
        }
    }

    /// <summary>
    /// Merge with another errors dictionary.
    /// </summary>
    /// <param name="errorsDictionary">Another errors dictionary.</param>
    public void Merge(IDictionary<string, ICollection<string>> errorsDictionary)
    {
        foreach (var errors in errorsDictionary)
        {
            AddErrors(errors.Key, errors.Value);
        }
    }

    /// <summary>
    /// Validate object with annotation attributes and creates <see cref="ValidationErrors"/> object.
    /// </summary>
    /// <param name="instance">Object to validate.</param>
    /// <param name="serviceProvider">Service provider.</param>
    /// <returns></returns>
    public static ValidationErrors CreateWithAttributeValidation(object? instance, IServiceProvider serviceProvider)
    {
        if (instance == null)
        {
            throw new ArgumentNullException(nameof(instance));
        }
        
        var validationErrors = new ValidationErrors();

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(instance, serviceProvider, null);
        var hasErrors = Validator.TryValidateObject(instance, validationContext, validationResults);

        if (!hasErrors)
        {
            foreach (var validationResult in validationResults)
            {
                foreach (var memberName in validationResult.MemberNames)
                {
                    validationErrors.AddError(memberName, validationResult.ErrorMessage);
                }
            }
        }
        
        return validationErrors;
    }
}