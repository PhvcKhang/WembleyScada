
using WembleyScada.Domain.SeedWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .WithOrigins("localhost",
                         "http://localhost:3000",
                         "http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("WembleyScada.Api"));
    options.EnableSensitiveDataLogging();
});

builder.Services.AddAutoMapper(typeof(ModelToViewModelProfile));
builder.Services.AddAutoMapper(typeof(ViewModelToModelProfile));
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<ModelToViewModelProfile>();
    cfg.RegisterServicesFromAssemblyContaining<ViewModelToModelProfile>();
    cfg.RegisterServicesFromAssemblyContaining<ApplicationDbContext>();
    cfg.RegisterServicesFromAssemblyContaining<Entity>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
