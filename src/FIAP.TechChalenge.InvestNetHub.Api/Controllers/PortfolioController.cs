using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.CreatePortfolio;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.DeletePortfolio;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.GetPortfolio;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.ListPortfolios;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.UpdatePortfolio;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;
using MediatR;
using FIAP.TechChalenge.InvestNetHub.Api.ApiModels.Response;
using FIAP.TechChalenge.InvestNetHub.Api.Extensions;
using FIAP.TechChalenge.InvestNetHub.Api.ApiModels.Portfolio;

namespace FIAP.TechChalenge.InvestNetHub.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PortfoliosController : ControllerBase
    {
        private readonly ILogger<PortfoliosController> _logger;
        private readonly IMediator _mediator;

        public PortfoliosController(
            ILogger<PortfoliosController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<PortfolioModelOutput>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(
            [FromBody] CreatePortfolioApiInput apiInput,
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Forbid();
            }

            var input = new CreatePortfolioInput(
                userId.Value, 
                apiInput.Name,
                apiInput.Description
            );

            var result = await _mediator.Send(input, cancellationToken);

            return CreatedAtAction(
                nameof(Create),
                new { result.Id },
                new ApiResponse<PortfolioModelOutput>(result)
            );
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<PortfolioModelOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetPortfolioInput(id),
                cancellationToken
            );

            return Ok(new ApiResponse<PortfolioModelOutput>(result));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Forbid();
            }

            var portfolio = await _mediator.Send(new GetPortfolioInput(id), cancellationToken);

            if (portfolio.UserId != userId)
            {
                return Forbid();
            }

            var input = new DeletePortfolioInput(id);
            await _mediator.Send(input, cancellationToken);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<PortfolioModelOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdatePortfolioInput apiInput,
            CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Forbid();
            }

            var portfolio = await _mediator.Send(new GetPortfolioInput(id), cancellationToken);

            if (portfolio.UserId != userId)
            {
                return Forbid();
            }

            var input = new UpdatePortfolioInput(
                id,
                apiInput.Name,
                apiInput.Description
            );
            var result = await _mediator.Send(input, cancellationToken);
            return Ok(new ApiResponse<PortfolioModelOutput>(result));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListPortfoliosOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> List(
            CancellationToken cancellation,
            [FromQuery] int? page = null,
            [FromQuery(Name = "per_page")] int? perPage = null,
            [FromQuery] string? search = null,
            [FromQuery] string? sort = null,
            [FromQuery] SearchOrder? dir = null)
        {
            var input = new ListPortfoliosInput();
            if (page.HasValue)
                input.Page = page.Value;
            if (perPage.HasValue)
                input.PerPage = perPage.Value;
            if (!string.IsNullOrWhiteSpace(search))
                input.Search = search;
            if (!string.IsNullOrWhiteSpace(sort))
                input.Sort = sort;
            if (dir.HasValue)
                input.Dir = dir.Value;

            var output = await _mediator.Send(input, cancellation);

            return Ok(new ApiResponseList<PortfolioModelOutput>(output));
        }
    }
}
