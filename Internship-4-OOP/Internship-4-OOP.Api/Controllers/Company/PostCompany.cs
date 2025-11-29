using Internship_4_OOP.Application.Companies.Commands.CreateCompany;
using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Application.Users.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Internship_4_OOP.Api.Controllers.Company;

[ApiController]
[Route("api/companies")]
public class PostCompany(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCompanyDto dto)
    {

        var result = await mediator.Send(CreateCompanyCommand.FromDto(dto));
        if (result.IsFailure)
            return BadRequest(result.Error);
        return Ok(result.Value);
    }
}
