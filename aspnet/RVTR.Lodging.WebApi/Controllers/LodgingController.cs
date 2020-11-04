using System;
using System.Collections.Generic;
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
  /// The LodgingController handles lodging resources
  /// </summary>
  [ApiController]
  [ApiVersion("0.0")]
  [EnableCors("public")]
  [Route("rest/lodging/{version:apiVersion}/[controller]")]
  public class LodgingController : ControllerBase
  {
    private readonly ILogger<LodgingController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Constructor for the LodgingController sets up logger and unitOfWork dependencies
    /// </summary>
    /// <param name="logger">The Logger</param>
    /// <param name="unitOfWork">The UnitOfWork</param>
    public LodgingController(ILogger<LodgingController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Gets all the lodgings in the database
    /// </summary>
    /// <returns>The Lodgings</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LodgingModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
      _logger.LogInformation($"Getting all lodgings...");
      return Ok(await _unitOfWork.Lodging.SelectAsync());
    }

    /// <summary>
    /// Gets one Lodging based on its id
    /// </summary>
    /// <param name="id">The Lodging Id</param>
    /// <returns>The Lodgings if successful or NotFound if no lodging was found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LodgingModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        _logger.LogInformation($"Getting a lodging @ id = {id}...");
        var lodging = await _unitOfWork.Lodging.SelectAsync(id);
        if (lodging == null)
        {
          throw new KeyNotFoundException("The given id does not exist");
        }
        else
        {
          _logger.LogInformation($"Successfully found a lodging @ id = {id}.");
          return Ok(lodging);
        }
      } catch (KeyNotFoundException e)
      {
        _logger.LogInformation($"Could not find lodging @ id = {id}. Caught: {e.Message}");
        return NotFound(id);
      }
    }

    /// <summary>
    /// Gets all lodgings with available rentals by City, State/Province, Country and occupancy
    /// </summary>
    /// <param name="city">The city</param>
    /// <param name="state">The state/province</param>
    /// <param name="country">The country</param>
    /// <param name="occupancy">The occupancy</param>
    /// <returns>The filtered Lodgings</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LodgingModel>), StatusCodes.Status200OK)]
    [Route("available")]
    public async Task<IActionResult> GetLodgingsByLocationAndOccupancy(string city, string state, string country, int occupancy)
    {
      _logger.LogInformation($"Getting all available lodgings matching City: {city}, State: {state}, Country: {country}, Occupancy: {occupancy}...");
      return Ok(await _unitOfWork.Lodging.LodgingByLocationAndOccupancy(occupancy, city, state, country));
    }

    /// <summary>
    /// Action method for deleting a lodging by lodging id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        _logger.LogInformation($"Deleting a lodging @ id = {id}...");
        LodgingModel lodge = await _unitOfWork.Lodging.SelectAsync(id);
        if (lodge == null)
        {
          throw new KeyNotFoundException("The given id was not found.");
        }
        else
        {
          await _unitOfWork.Lodging.DeleteAsync(lodge.Id);
          await _unitOfWork.CommitAsync();
          _logger.LogInformation($"Successfully deleted a lodging @ id = {lodge.Id}.");
          return Ok();
        }
      }
      catch (KeyNotFoundException e)
      {
        _logger.LogInformation($"Could not find lodging @ id = {id}. Caught: {e.Message}");
        return NotFound(id);
      }
    }

    /// <summary>
    /// Action method for creating a new lodging
    /// </summary>
    /// <param name="lodging"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(LodgingModel lodging)
    {
      _logger.LogInformation($"Creating a new lodging @ {lodging}...");
      await _unitOfWork.Lodging.InsertAsync(lodging);
      await _unitOfWork.CommitAsync();
      _logger.LogInformation($"Successfully created a new lodging @ {lodging}.");
      return Accepted(lodging);
    }

    /// <summary>
    /// Action method for updating a preexisting lodging
    /// </summary>
    /// <param name="lodging"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(LodgingModel lodging)
    {
      try
      {
        _logger.LogInformation($"Updating a lodging @ {lodging}...");
        var newlodging = await _unitOfWork.Lodging.SelectAsync(lodging.Id);
        if (newlodging == null)
        {
          throw new KeyNotFoundException("The given id was not found.");
        }
        else
        {
          _unitOfWork.Lodging.Update(newlodging);
          await _unitOfWork.CommitAsync();
          _logger.LogInformation($"Successfully updated a lodging @ {newlodging}.");
          return Accepted(lodging);
        }
      }
      catch (NullReferenceException e)
      {
        _logger.LogInformation($"Failed to update a lodging @ {lodging}. Caught: {e}.");
        return NotFound(lodging);
      }
      catch (KeyNotFoundException e)
      {
        _logger.LogInformation($"Could not find lodging @ id = {lodging.Id}. Caught: {e.Message}");
        return NotFound(lodging.Id);
      }
    }
  }
}
