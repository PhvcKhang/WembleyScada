

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<ApplicationDbContext>();
            cfg.RegisterServicesFromAssemblyContaining<Entity>();
        });

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("WembleyScada.Api"));
            options.EnableSensitiveDataLogging();
        });

        var config = builder.Configuration;
        services.Configure<MqttOptions>(config.GetSection("MqttOptions"));
        services.AddSingleton<ManagedMqttClient>();

        services.AddSingleton<MetricMessagePublisher>();
        services.AddSingleton<ExecutionTimeBuffers>();
        services.AddSingleton<StatusTimeBuffers>();

        services.AddHostedService<UpdateShiftReportWorker>();
    })
    .Build();

await host.RunAsync();
