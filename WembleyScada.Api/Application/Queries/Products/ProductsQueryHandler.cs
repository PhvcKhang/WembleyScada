

namespace WembleyScada.Api.Application.Queries.Products;

public class ProductsQueryHandler : IRequestHandler<ProductsQuery, IEnumerable<ProductViewModel>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProductsQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductViewModel>> Handle(ProductsQuery request, CancellationToken cancellationToken)
    {
        var line = await _context.Lines
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.LineId == request.LineId);

        var queryable = _context.Products
                    .AsNoTracking();

        if (request.LineId is not null && line is not null)
        {
            queryable = queryable.Where(x => x.UsableLines.Contains(line));
        }

        var products = await queryable.ToListAsync();

        var viewModels = _mapper.Map<IEnumerable<ProductViewModel>>(products);

        return viewModels;
    }
}
