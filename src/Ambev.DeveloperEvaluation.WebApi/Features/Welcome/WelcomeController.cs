using Ambev.DeveloperEvaluation.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Welcome;

/// <summary>
/// Controller for managing user operations
/// </summary>
[ApiController]
[Route("")]
public class WelcomeController : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult WelcomePost()
    {
        return Ok("Welcome to Ambev API - Post");
    }

    [HttpGet]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public IActionResult WelcomeGet()
    {
        return Ok("Welcome to Ambev API - Get");
    }
}
