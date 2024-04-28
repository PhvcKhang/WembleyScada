IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<UpdateShiftReportWorker>();
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
        services.Configure<OneSignalOptions>(config.GetSection("OneSignalOptions"));
        services.AddSingleton<ManagedMqttClient>();

        services.AddScoped<IStationRepository, StationRepository>();
        services.AddScoped<IShiftReportRepository, ShiftReportRepository>();
        services.AddScoped<IMachineStatusRepository, MachineStatusRepository>();
        services.AddScoped<IErrorInformationRepository, ErrorInformationRepository>();

        services.AddSingleton<MetricMessagePublisher>();
        services.AddSingleton<OneSignalHelper>();
        services.AddSingleton<ExecutionTimeBuffers>();
        services.AddSingleton<StatusTimeBuffers>();

        services.AddHostedService<UpdateShiftReportWorker>();
    })
    .Build();

await host.RunAsync();
