using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.Common;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.CreateUser;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.DeleteUser;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.GetUser;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.TechChalenge.InvestNetHub.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IMediator _mediator;

    public UsersController(
        ILogger<UsersController> logger,
        IMediator mediator
        )
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserModelOutput), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserInput input,
        CancellationToken cancellationToken
        )
    {
        var result = await _mediator.Send(input, cancellationToken);
        return CreatedAtAction(
            nameof(Create), 
            new { result.Id}, 
            result
        );
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(UserModelOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(
            new GetUserInput(id), 
            cancellationToken
        );

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        await _mediator.Send(
            new DeleteUserInput(id), 
            cancellationToken
        );

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UserModelOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(
        [FromBody] UpdateUserInput input,
        CancellationToken cancellationToken
    )
    {
        var result = await _mediator.Send(input, cancellationToken);
        return Ok(result);
    }
}
