using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using OrleansNet6Demo.Interfaces.Grains;
using static System.Console;

var client = new ClientBuilder()
    .UseLocalhostClustering()
    .Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "extension-demo";
        options.ServiceId = "Demo Grain Extension";
    })
    .ConfigureApplicationParts(parts =>
    {
        parts.AddApplicationPart(typeof(IHelloGrain).Assembly).WithReferences();
        parts.AddApplicationPart(typeof(ICounterGrain).Assembly).WithReferences();
    })
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
    .Build();

WriteLine(
    "\r\n---\r\nPlease wait until Orleans Server is started and ready for connections, then press any key to start connect...\r\n---\r\n");
ReadKey();
await client.Connect();

WriteLine("\r\n---\r\nOrleans Client connected, press any key to start Demo...\r\n---");
ReadKey();

var helloGrain = client.GetGrain<IHelloGrain>(0);
var helloResult = await helloGrain.SayHello("Hello Grain Extension");
WriteLine($"\r\nhelloGrain.SayHello = {helloResult}\r\n");
var counterGrain01 = client.GetGrain<ICounterGrain>("My Demo Counter 1");
var counterGrain02 = client.GetGrain<ICounterGrain>("My Demo Counter 2");
await counterGrain01.Increment();
await counterGrain02.Increment(2);
var result01 = await counterGrain01.GetCount();
var result02 = await counterGrain02.GetCount();
WriteLine($"Increment counter 01 to {result01}, counter 02 to {result02}");

WriteLine("\r\n---\r\nPress any key to continue Grain Extension Demo ...\r\n---\r\n");
ReadKey();

WriteLine("\r\n---\r\nGet Grain Info ...\r\n---\r\n");
var helloGrainInfo = await helloGrain.GetGrainInfo();
var counterGrain01Info = await counterGrain01.GetGrainInfo();
var counterGrain02Info = await counterGrain02.GetGrainInfo();
WriteLine($"helloGrain = \r\n{helloGrainInfo}");
WriteLine($"counterGrain01 = \r\n{counterGrain01Info}");
WriteLine($"counterGrain02 = \r\n{counterGrain02Info}");

WriteLine("\r\n---\r\nDemonstration finished, press any key to exit...\r\n---");
ReadKey();

await client.Close();
client.Dispose();