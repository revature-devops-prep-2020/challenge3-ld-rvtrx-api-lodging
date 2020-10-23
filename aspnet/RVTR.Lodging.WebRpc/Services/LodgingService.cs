using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using RVTR.Lodging.ObjectModel.Interfaces;

namespace RVTR.Lodging.WebRpc
{
  public class LodgingService : Lodging.LodgingBase
  {
    private readonly ILogger<LodgingService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public LodgingService(ILogger<LodgingService> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    public override Task<LodgingResponse> Book(LodgingRequest request, ServerCallContext context)
    {
      return Task.FromResult(new LodgingResponse()
      {
        Message = "Hello " + request.Name
      });
    }
  }
}
