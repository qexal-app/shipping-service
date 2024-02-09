using Google.Protobuf;
using Grpc.Core;
using Qexal;

namespace ManualShippingProvider.Services;

public class ShippingProviderService(IWebHostEnvironment environment) : ShippingProvider.ShippingProviderBase
{
    public override Task<ValidateAddressResponse> ValidateAddress(ValidateAddressRequest request, ServerCallContext context)
    {
        var result = new ValidateAddressResponse
        {
            Status = AddressValidationStatus.Verified,
            OriginalAddress = request.Address,
            MatchedAddress = request.Address,
            ServiceName = request.ServiceName
        };

        return Task.FromResult(result);
    }

    public override async Task<ShippingLabelResponse> Label(ShippingLabelRequest request, ServerCallContext context)
    {
        var bytes = await File.ReadAllBytesAsync(Path.Combine(environment.WebRootPath, "label.pdf"));
        return new ShippingLabelResponse { Document = ByteString.CopyFrom(bytes) };
    }

    public override Task<ShippingRegisterResponse> Save(ShippingRegisterRequest request, ServerCallContext context)
    {
        var result = new ShippingRegisterResponse
        {
            Shipment = new ShipmentResponse
            {
                TrackingNumber = "1234 5678 9012 3456 7890 12"
            }
        };

        return Task.FromResult(result);
    }

    public override Task<CancelShipmentResponse> Cancel(CancelShipmentRequest request, ServerCallContext context)
    {
        return Task.FromResult(new CancelShipmentResponse { Success = true });
    }
}