namespace BusinessBooster.ToDo.UseCases;

/// <summary>
/// The entity id wrapper.
/// </summary>
/// <typeparam name="T">Type of id.</typeparam>
public class IdDto<T>
{
    /// <summary>
    /// Entity id.
    /// </summary>
    public T Id { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="id">Entity id.</param>
    public IdDto(T id)
    {
        Id = id;
    }
    
    /// <summary>
    /// Implicit convert to type. 
    /// </summary>
    public static implicit operator IdDto<T>(T value)
    {
        return new IdDto<T>(value);
    }
}