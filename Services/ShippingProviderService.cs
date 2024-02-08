using Grpc.Core;
using Qexal;

namespace ManualShippingProvider.Services;

public class ShippingProviderService(ILogger<ShippingProviderService> logger) : ShippingProvider.ShippingProviderBase
{
    public override Task<ShippingLabelResponse> Label(ShippingLabelRequest request, ServerCallContext context)
    {
        return Task.FromResult(new ShippingLabelResponse { DocumentUrl = "https://google.com" });
    }

    public override Task<ShippingRegisterResponse> Save(ShippingRegisterRequest request, ServerCallContext context)
    {
        var result = new ShippingRegisterResponse
        {
            Shipment = new ShipmentResponse
            {
                TrackingNumber = request.OrderNumber
            }
        };

        return Task.FromResult(result);
    }

    public override Task<CancelShipmentResponse> Cancel(CancelShipmentRequest request, ServerCallContext context)
    {
        return Task.FromResult(new CancelShipmentResponse { Success = true });
    }
}