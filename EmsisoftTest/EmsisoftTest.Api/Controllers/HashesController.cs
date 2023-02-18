using EmsisoftTest.Api.Handlers;
using EmsisoftTest.Api.Handlers.GenerateHash;
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
    public async Task<IActionResult> GetHashes([FromQuery] int count = 40000)
    {
        var request = new GenerateHashesRequest(count);
        await _mediator.Send(request);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateHashes()
    {
        return Ok();
    }
}