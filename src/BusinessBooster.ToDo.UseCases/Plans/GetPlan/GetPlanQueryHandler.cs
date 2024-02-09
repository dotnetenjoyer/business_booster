using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessBooster.ToDo.Domain.Exceptions;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Database;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Services;
using BusinessBooster.ToDo.UseCases.Plans.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessBooster.ToDo.UseCases.Plans.GetPlan;

/// <summary>
/// Query handler for <see cref="GetPlanQuery"/>
/// </summary>
internal class GetPlanQueryHandler : IRequestHandler<GetPlanQuery, DetailedPlanDto>
{
    private readonly IDbContext dbContext;
    private readonly ILoggedUserAccessor loggedUserAccessor;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetPlanQueryHandler(IDbContext dbContext, ILoggedUserAccessor loggedUserAccessor, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.loggedUserAccessor = loggedUserAccessor;
        this.mapper = mapper;
    }

    /// <inhertidoc />
    public async Task<DetailedPlanDto> Handle(GetPlanQuery query, CancellationToken cancellationToken)
    {
        var loggedUserId = loggedUserAccessor.GetCurrentUserId();
        var plan = await dbContext.Plans
            .Include(x => x.Tasks)
            .ThenInclude(x => x.TaskStatusRecords)
            .Where(x => x.Id == query.Id && x.UserId == loggedUserId)
            .ProjectTo<DetailedPlanDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (plan == null)
        {
            throw new NotFoundException("No plan with the specified identifier was found.");
        }

        return plan;
    }
}