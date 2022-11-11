using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SubForumsController : ControllerBase
{
    private readonly ISubForumLogic subForumLogic;

    public SubForumsController(ISubForumLogic subForumLogic)
    {
        this.subForumLogic = subForumLogic;
    }

    [HttpPost]
    public async Task<ActionResult<SubForum>> CreateAsync(SubForumCreationDto dto)
    {
        try
        {
            SubForum created = await subForumLogic.CreateAsync(dto);
            return Created($"/subForum/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubForum>>> GetAsync([FromQuery] string? userName, [FromQuery] int? userId,
        [FromQuery] string? titleContains)
    {
        try
        {
            SearchSubForumParametersDto parameters = new(userName, userId, titleContains);
            var subForums = await subForumLogic.GetAsync(parameters);
            return Ok(subForums);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] SubForumUpdateDto dto)
    {
        try
        {
            await subForumLogic.UpdateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await subForumLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}