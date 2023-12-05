using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using OrleansNet6Demo.Grains.Counter;
using OrleansNet6Demo.Grains.Hello;
using OrleansNet6Demo.Interface.GrainExtensions;
using OrleansNet6Demo.Silo.WorkerService.GrainExtensions;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddLogging(logBuilder =>
        {
            logBuilder.ClearProviders();
            logBuilder.AddConsole();
            if (System.Diagnostics.Debugger.IsAttached)
            {
                logBuilder.AddDebug();
            }
        });
    })
    .UseOrleans(siloBuilder =>
    {
        siloBuilder.UseLocalhostClustering()
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "extension-demo";
                options.ServiceId = "Demo Grain Extension";
            })
            .ConfigureApplicationParts(parts =>
            {
                parts.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences();
                parts.AddApplicationPart(typeof(CounterGrain).Assembly).WithReferences();
            })
            .AddAzureTableGrainStorageAsDefault(options =>
            {
                options.UseJson = true;
                options.ConfigureTableServiceClient("UseDevelopmentStorage=true");
            })
            .AddGrainExtension<IGrainInfoCollector, GrainInfoCollector>();
    })
    .Build();

await host.RunAsync();