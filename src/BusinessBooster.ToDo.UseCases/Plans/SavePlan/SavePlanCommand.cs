using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace BusinessBooster.ToDo.UseCases.Plans.SavePlan;

/// <summary>
/// Command to save plan.
/// </summary>
public class SavePlanCommand : IRequest<IdDto<int>>
{
    /// <summary>
    /// Id.
    /// </summary>
    [DisplayName("Id")]
    public int? Id { get; set; }
    
    /// <summary>
    /// Plan name.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [DisplayName("Name")]
    public string? Name { get; set; }
    
    [DisplayName("Description")]
    public string? Description { get; set; }
}