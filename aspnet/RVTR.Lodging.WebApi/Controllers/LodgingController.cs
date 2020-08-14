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
        public LodgingController(ILogger<LodgingController> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var lodgings = await _unitOfWork.Lodging.SelectAsync();
            if (lodgings == null)
                return NotFound();
            else
                return Ok(lodgings);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound(id);
            }
            else
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
        [Route("getAvailableLodgings")]
        public async Task<IActionResult> GetAvailableLodgings()
        {
            return Ok(await _unitOfWork.Lodging.AvailableLodgings());
        }
    }
}
