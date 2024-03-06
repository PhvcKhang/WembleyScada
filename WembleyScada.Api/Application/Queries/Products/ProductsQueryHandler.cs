

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
        var lines = await _context.Lines
            .AsNoTracking()
            .Where(x => x.LineType == request.LineType)
            .ToListAsync();

        var queryable = _context.Products
                    .AsNoTracking();

        if (request.LineType is not null && lines is not null)
        {
            foreach (var line in lines)
            {
                queryable = queryable.Where(x => x.UsableLines.Contains(line));
            }
        }

        var products = await queryable.ToListAsync();

        var viewModels = _mapper.Map<IEnumerable<ProductViewModel>>(products);

        return viewModels;
    }
}
