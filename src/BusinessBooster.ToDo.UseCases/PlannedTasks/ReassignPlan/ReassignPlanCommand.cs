using System.ComponentModel.DataAnnotations;
using MediatR;

namespace BusinessBooster.ToDo.UseCases.PlannedTasks.ReassignPlan;

/// <summary>
/// Command to reassign task plan.
/// </summary>
public class ReassignPlanCommand : IRequest
{   
    /// <summary>
    /// Task id.
    /// </summary>
    [Required]
    public int TaskId { get; set; }
    
    /// <summary>
    /// New plan id.
    /// </summary>
    [Required]
    public int PlanId { get; set; }
}