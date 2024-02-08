using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessBooster.ToDo.Domain.Exceptions;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Database;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BusinessBooster.ToDo.UseCases.Plans.GetPlan;

/// <summary>
/// Query handler for <see cref="GetPlanQuery"/>
/// </summary>
internal class GetPlanQueryHandler : IRequestHandler<GetPlanQuery, PlanDto>
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
    public async Task<PlanDto> Handle(GetPlanQuery query, CancellationToken cancellationToken)
    {
        var loggedUserId = loggedUserAccessor.GetCurrentUserId();
        
        var plan = await dbContext.Plans
            .Where(x => x.Id == query.Id && x.UserId == loggedUserId)
            .ProjectTo<PlanDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (plan == null)
        {
            throw new NotFoundException("No plan with the specified identifier was found.");
        }

        return plan;
    }
}