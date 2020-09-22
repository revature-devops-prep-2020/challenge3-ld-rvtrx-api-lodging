using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Lodging.DataContext.Repositories;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.WebApi.Controllers
{
  /// <summary>
  ///
  /// </summary>
  [ApiController]
  [ApiVersion("0.0")]
  [EnableCors("public")]
  [Route("rest/lodging/{version:apiVersion}/[controller]")]
  public class ImagesController : ControllerBase
  {
    private readonly ILogger<ImagesController> _logger;
    private readonly UnitOfWork _unitOfWork;

    /// <summary>
    ///
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public ImagesController(ILogger<ImagesController> logger, UnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      return Ok(await Task.FromResult<string[]>(new string[] { "http://placecorgi.com/1280/960", "http://placecorgi.com/1280/960", "http://placecorgi.com/1280/960", "http://placecorgi.com/1280/960", "http://placecorgi.com/1280/960", "http://placecorgi.com/1280/960", "http://placecorgi.com/1280/960" }));
    }
  }
}
