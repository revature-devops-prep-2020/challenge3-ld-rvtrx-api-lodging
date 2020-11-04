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
  /// Rental controller
  /// </summary>
  [ApiController]
  [ApiVersion("0.0")]
  [EnableCors("public")]
  [Route("rest/lodging/{version:apiVersion}/[controller]")]
  public class RentalController : ControllerBase
  {
    private readonly ILogger<RentalController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Constructor of the rental controller
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public RentalController(ILogger<RentalController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Action method for deleting a rental by rental id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        _logger.LogInformation($"Deleting a rental @ id = {id}...");
        var rental = await _unitOfWork.Rental.SelectAsync(id);
        if (rental == null)
        {
          throw new KeyNotFoundException("The given id does not exist.");
        }
        else
        {
          await _unitOfWork.Rental.DeleteAsync(id);
          await _unitOfWork.CommitAsync();
          _logger.LogInformation($"Successfully deleted a rental @ id = {id}.");
          return Ok();
        }
      }
      catch (KeyNotFoundException e)
      {
        _logger.LogInformation($"Could not find rental @ id = {id}. Caught: {e.Message}");
        return NotFound(id);
      }
    }

    /// <summary>
    /// Action method for getting all rentals
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      _logger.LogInformation($"Getting all rentals...");
      return Ok(await _unitOfWork.Rental.SelectAsync());
    }

    /// <summary>
    /// Action method for getting a rental by rental id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        _logger.LogInformation($"Getting a rental @ id = {id}...");
        var rental = await _unitOfWork.Rental.SelectAsync(id);
        if (rental == null)
        {
          throw new KeyNotFoundException("The given id does not exist");
        }
        else
        {
          return Ok(rental);
        }
      }
      catch (KeyNotFoundException e)
      {
        _logger.LogInformation($"Could not find rental @ id = {id}. Caught: {e.Message}");
        return NotFound(id);
      }
    }

    /// <summary>
    /// Action method for creating a new rental
    /// </summary>
    /// <param name="rental"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(RentalModel rental)
    {
      _logger.LogInformation($"Creating a new rental @ {rental}...");
      await _unitOfWork.Rental.InsertAsync(rental);
      await _unitOfWork.CommitAsync();
      _logger.LogInformation($"Successfully created a new rental @ {rental}.");
      return Accepted(rental);
    }

    /// <summary>
    /// Action method for updating a preexisting rental
    /// </summary>
    /// <param name="rental"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(RentalModel rental)
    {
      try
      {
        _logger.LogInformation($"Updating a rental @ {rental}...");
        var selectedRental = await _unitOfWork.Rental.SelectAsync(rental.Id);
        if (selectedRental == null)
        {
          throw new KeyNotFoundException("The given id was not found.");
        }
        else
        {
          _unitOfWork.Rental.Update(selectedRental);
          await _unitOfWork.CommitAsync();
          _logger.LogInformation($"Successfully updated a rental @ {selectedRental}.");
          return Accepted(selectedRental);
        }
      }
      catch (NullReferenceException e)
      {
        _logger.LogInformation($"Failed to update a rental @ {rental}. Caught: {e}.");
        return NotFound(rental);
      }
      catch (KeyNotFoundException e)
      {
        _logger.LogInformation($"Could not find rental @ id = {rental.Id}. Caught: {e.Message}");
        return NotFound(rental.Id);
      }
    }
  }
}
