using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Lodging.DataContext.Repositories;
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
        private readonly UnitOfWork _unitOfWork;
        private readonly LodgingRepo _lodgingRepo;

        /// <summary>
        /// Constructor for the LodgingController sets up logger and unitOfWork dependencies
        /// </summary>
        /// <param name="logger">The Logger</param>
        /// <param name="unitOfWork">The UnitOfWork</param>
        public LodgingController(ILogger<LodgingController> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all the lodgings in the database
        /// </summary>
        /// <returns>The Lodgings if successful or NotFound if there are no lodgings</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var lodgings = await _unitOfWork.Lodging.SelectAsync();
            if (lodgings.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(lodgings);
            }
        }

        /// <summary>
        /// Gets one Lodging based on its id
        /// </summary>
        /// <param name="id">The Lodging Id</param>
        /// <returns>The Lodgings if successful or NotFound if no lodging was found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var lodging = await _unitOfWork.Lodging.SelectAsync(id);
            if(lodging != null)
            {
                return Ok(lodging);
            }
            else
            {
                return NotFound(id);
            }
        }

        /// <summary>
        /// Gets all lodgings with available rentals by city and occupancy
        /// </summary>
        /// <param name="city">The city</param>
        /// <param name="occupancy">The occupancy</param>
        /// <returns>The filtered Lodgings</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("available")]
        public async Task<IActionResult> getLodgingsByCityAndOccupancy(string city, int occupancy)
        {
            var lodgings = await _unitOfWork.Lodging.LodgingByCityAndOccupancy(city, occupancy);
            if(lodgings.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(lodgings);
            }
            
        }
    }
}

