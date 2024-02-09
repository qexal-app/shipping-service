using System.Net;
using ManualShippingProvider.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddGrpc();

if (builder.Environment.IsDevelopment())
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.Listen(IPAddress.Loopback, 7070, listenOptions =>
        {
            listenOptions.Protocols = HttpProtocols.Http2;
            listenOptions.UseHttps();
        });
    });
}

var app = builder.Build();

app.MapGrpcService<ShippingProviderService>();

app.Run();