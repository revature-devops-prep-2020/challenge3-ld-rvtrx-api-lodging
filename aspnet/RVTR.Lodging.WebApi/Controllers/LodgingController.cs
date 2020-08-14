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
  public class LodgingController : ControllerBase
  {
    private readonly ILogger<LodgingController> _logger;
    private readonly UnitOfWork _unitOfWork;
    private readonly LodgingRepo _lodgingRepo;

    /// <summary>
    ///
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    /// <param name="lodgingRepo"></param>
    public LodgingController(ILogger<LodgingController> logger, UnitOfWork unitOfWork, LodgingRepo lodgingRepo)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
      _lodgingRepo = lodgingRepo;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        await _unitOfWork.Lodging.DeleteAsync(id);
        await _unitOfWork.CommitAsync();

        return Ok();
      }
      catch
      {
        return NotFound(id);
      }
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      return Ok(await _unitOfWork.Lodging.SelectAsync());
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        return Ok(await _unitOfWork.Lodging.SelectAsync(id));
      }
      catch
      {
        return NotFound(id);
      }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="lodging"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(LodgingModel lodging)
    {
      await _unitOfWork.Lodging.InsertAsync(lodging);
      await _unitOfWork.CommitAsync();

      return Accepted(lodging);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="lodging"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> Put(LodgingModel lodging)
    {
      _unitOfWork.Lodging.Update(lodging);
      await _unitOfWork.CommitAsync();

      return Accepted(lodging);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAvailableLodgings()
    {
      return Ok(await _lodgingRepo.AvailableLodgings());
    }
  }
}
