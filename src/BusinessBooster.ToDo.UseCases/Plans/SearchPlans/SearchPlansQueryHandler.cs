using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Database;
using BusinessBooster.ToDo.Infrastructure.Abstraction.Services;
using BusinessBooster.ToDo.UseCases.Common.Pagination;
using BusinessBooster.ToDo.UseCases.Plans.Dtos;

namespace BusinessBooster.ToDo.UseCases.Plans.SearchPlans;

/// <summary>
/// Handler for search <see cref="SearchPlansQuery"/>.
/// </summary>
public class SearchPlansQueryHandler : IRequestHandler<SearchPlansQuery, PagedListMetadataDto<PlanDto>>
{
    private readonly IDbContext dbContext;
    private readonly ILoggedUserAccessor loggedUserAccessor;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    public SearchPlansQueryHandler(IDbContext dbContext, ILoggedUserAccessor loggedUserAccessor, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.loggedUserAccessor = loggedUserAccessor;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<PagedListMetadataDto<PlanDto>> Handle(SearchPlansQuery query, CancellationToken cancellationToken)
    {
        var loggedUserId = loggedUserAccessor.GetCurrentUserId();
        
        var databaseQuery = dbContext.Plans
            .Where(x => x.UserId == loggedUserId);

        if (!string.IsNullOrEmpty(query.Filter))
        {
            databaseQuery = databaseQuery.Where(x => x.Name.Contains(query.Filter));
        }

        var finalQuery = databaseQuery
            .ProjectTo<PlanDto>(mapper.ConfigurationProvider);


        var pagedList = await EFCorePagedListFactory
            .CreateAsync(finalQuery, query.Page, query.PageSize, cancellationToken);

        return pagedList.ToMetadataObject();
    }
}