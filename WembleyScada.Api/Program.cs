
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

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

var configure = builder.Configuration;

builder.Services.Configure<MqttOptions>(configure.GetSection("MqttOptions"));

builder.Services.AddHostedService<ScadaHost>();

builder.Services.AddSingleton<ManagedMqttClient>();
builder.Services.AddSingleton<Buffer>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IStationRepository, StationRepository>();
builder.Services.AddScoped<IStationReferenceRepository, StationReferenceRepository>();
builder.Services.AddScoped<IReferenceRepository, ReferenceRepository>();

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/NotificationHub");

app.Run();
