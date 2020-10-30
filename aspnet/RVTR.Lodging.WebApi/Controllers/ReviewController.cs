using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Lodging.ObjectModel.Interfaces;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.WebApi.Controllers
{
  /// <summary>
  /// Review controller
  /// </summary>
  [ApiController]
  [ApiVersion("0.0")]
  [EnableCors("public")]
  [Route("rest/lodging/{version:apiVersion}/[controller]")]
  public class ReviewController : ControllerBase
  {
    private readonly ILogger<ReviewController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Constructor of the review controller
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public ReviewController(ILogger<ReviewController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Action method for deleting a review by review id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        _logger.LogInformation($"Deleting a review @ id = {id}...");
        await _unitOfWork.Review.DeleteAsync(id);
        await _unitOfWork.CommitAsync();
        _logger.LogInformation($"Successfully deleted a review @ id = {id}.");
        return Ok();
      }
      catch
      {
        _logger.LogInformation($"Could not delete review @ id = {id}.");
        return NotFound(id);
      }
    }

    /// <summary>
    /// Action method for getting all reviews
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      _logger.LogInformation($"Getting all reviews...");
      return Ok(await _unitOfWork.Review.SelectAsync());
    }

    /// <summary>
    /// Action method for getting a review by review id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        _logger.LogInformation($"Getting a review @ id = {id}...");
        return Ok(await _unitOfWork.Review.SelectAsync(id));
      }
      catch
      {
        _logger.LogInformation($"Could not find review @ id = {id}.");
        return NotFound(id);
      }
    }

    /// <summary>
    /// Action method for creating a new review
    /// </summary>
    /// <param name="review"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(ReviewModel review)
    {
      _logger.LogInformation($"Creating a new review @ {review}...");
      await _unitOfWork.Review.InsertAsync(review);
      await _unitOfWork.CommitAsync();
      _logger.LogInformation($"Successfully created a new review @ {review}.");
      return Accepted(review);
    }

    /// <summary>
    /// Action method for updating a preexisting review
    /// </summary>
    /// <param name="review"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(ReviewModel review)
    {
      try
      {
        _logger.LogInformation($"Updating a review @ {review}...");
        _unitOfWork.Review.Update(review);
        await _unitOfWork.CommitAsync();
        _logger.LogInformation($"Successfully updated a review @ {review}.");
        return Accepted(review);
      }
      catch
      {
        _logger.LogInformation($"Failed to update a review @ {review}.");
        return NotFound(review);
      }
    }
  }
}
