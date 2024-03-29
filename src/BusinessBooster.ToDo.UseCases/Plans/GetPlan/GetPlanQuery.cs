using BusinessBooster.ToDo.UseCases.Plans.Dtos;
using MediatR;

namespace BusinessBooster.ToDo.UseCases.Plans.GetPlan;

/// <summary>
/// Query to get plan.
/// </summary>
/// <param name="Id"></param>
public record GetPlanQuery(int Id) : IRequest<DetailedPlanDto>;