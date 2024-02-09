using AutoMapper;
using BusinessBooster.ToDo.Domain.Entities;
using BusinessBooster.ToDo.UseCases.PlannedTasks.SaveTask;

namespace BusinessBooster.ToDo.UseCases.PlannedTasks;

/// <summary>
/// Mapper task profile.
/// </summary>
public class TaskProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public TaskProfile()
    {
        CreateMap<SaveTaskCommand, PlannedTask>();
    }
}