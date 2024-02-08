using ManualShippingProvider.Services;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<ShippingProviderService>();

app.Run();