using FIAP.TechChalenge.InvestNetHub.Api.ApiModels.Response;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.Common;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.ListMarketNews;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.Common;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.ListUsers;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FIAP.TechChalenge.InvestNetHub.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class MarketNewsController : ControllerBase
{
    private readonly ILogger<MarketNewsController> _logger;
    private readonly IMediator _mediator;

    public MarketNewsController(
        ILogger<MarketNewsController> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ListMarketNewsOutput), StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetMarketNews(
    CancellationToken cancellation,
        [FromQuery] string? tickers = null,
        [FromQuery] string? topics = null,
        [FromQuery(Name = "from_time")] DateTime? fromTime = null,
        [FromQuery(Name = "to_time")] DateTime? toTime = null,
        [FromQuery] string? sort = null,
        [FromQuery] int limit = 50
    )
    {
        _logger.LogInformation(
            "Starting GetMarketNews with parameters:" + Environment.NewLine +
            "Tickers={Tickers}," + Environment.NewLine +
            "Topics={Topics}," + Environment.NewLine +
            "FromTime={FromTime}," + Environment.NewLine +
            "ToTime={ToTime}," + Environment.NewLine +
            "Sort={Sort}," + Environment.NewLine +
            "Limit={Limit}",
            tickers, topics, fromTime, toTime, sort, limit);

        var input = new ListMarketNewsInput(tickers, topics, fromTime, toTime, sort, limit);

        var result = await _mediator.Send(input, cancellation);

        _logger.LogInformation("GetMarketNews completed successfully.");

        return Ok(result);
    }
}
