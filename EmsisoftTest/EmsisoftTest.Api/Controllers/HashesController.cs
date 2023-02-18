using EmsisoftTest.Api.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmsisoftTest.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HashesController : ControllerBase
{
    private readonly IMediator _mediator;

    public HashesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetHashes()
    {
        var request = new GetHashesRequest();
        var hashes = await _mediator.Send(request);
        return Ok(hashes);
    }

    [HttpPost]
    public async Task<IActionResult> CreateHashes([FromQuery] int count = 40000)
    {
        var request = new GenerateHashesRequest(count);
        await _mediator.Send(request);
        return Ok();
    }
}