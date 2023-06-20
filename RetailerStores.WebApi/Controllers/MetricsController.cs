using MediatR;
using Microsoft.AspNetCore.Mvc;
using RetailerStores.Application.Dto;
using RetailerStores.Application.Metrics.Queries;

namespace RetailerStores.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetricsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MetricsController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get metrics total stock
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("getTotalStock")]
        [ProducesResponseType(typeof(StockMetricDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StockMetricDto>> GetTotalStock(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetTotalStockMetricQuery(), cancellationToken);
        }

        /// <summary>
        /// Get accuracy metrics
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("getStockAccuracy")]
        [ProducesResponseType(typeof(StockMetricDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StockMetricDto>> GetStockAccuracy(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetStockAccuracyMetricQuery(), cancellationToken);
        }

        /// <summary>
        /// Get on-floor-availability metrics
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("getStockOnFloorAvailability")]
        [ProducesResponseType(typeof(StockMetricDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StockMetricDto>> GetStockOnFloorAvailability(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetStockOnFloorAvailabilityMetricQuery(), cancellationToken);
        }

        /// <summary>
        /// Get mean age metrics
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("getStockMeanAge")]
        [ProducesResponseType(typeof(StockMetricDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StockMetricDto>> GetStockMeanAge(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetStockMeanAgeMetricQuery(), cancellationToken);
        }
    }
}
