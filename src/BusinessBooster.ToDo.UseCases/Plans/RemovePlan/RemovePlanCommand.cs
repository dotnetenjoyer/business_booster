using MediatR;

namespace BusinessBooster.ToDo.UseCases.Plans.RemovePlan;

/// <summary>
/// Command to remove plan.
/// </summary>
/// <param name="Id">Plan id.</param>
public record RemovePlanCommand(int Id) : IRequest;
