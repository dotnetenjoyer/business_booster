using AutoMapper;
using BusinessBooster.ToDo.Domain.Entities;
using BusinessBooster.ToDo.UseCases.Plans.GetPlan;
using BusinessBooster.ToDo.UseCases.Plans.SavePlan;

namespace BusinessBooster.ToDo.UseCases.Plans;

/// <summary>
/// Mapper profile for plans.
/// </summary>
public class PlansProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PlansProfile()
    {
        CreateMap<SavePlanCommand, Plan>();
        CreateMap<Plan, PlanDto>();
    }
}