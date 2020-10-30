using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Lodging.ObjectModel.Interfaces;

namespace RVTR.Lodging.WebApi.Controllers
{
  /// <summary>
  /// Images Controller
  /// </summary>
  [ApiController]
  [ApiVersion("0.0")]
  [EnableCors("public")]
  [Route("rest/lodging/{version:apiVersion}/[controller]")]
  public class ImagesController : ControllerBase
  {
    private readonly ILogger<ImagesController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Constructor of the images controller
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public ImagesController(ILogger<ImagesController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Action method for getting images by image id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      _logger.LogInformation($"Getting images @ id = {id}...");
      return Ok(await Task.FromResult<string[]>(new string[] { "http://placecorgi.com/1280/960", "http://placecorgi.com/1280/960", "http://placecorgi.com/1280/960", "http://placecorgi.com/1280/960", "http://placecorgi.com/1280/960", "http://placecorgi.com/1280/960", "http://placecorgi.com/1280/960" }));
    }
  }
}
