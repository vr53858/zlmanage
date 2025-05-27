using Microsoft.AspNetCore.Mvc;
using ZLManage.ApplicationServices.Services.Administrator;
using ZLManage.DomainModel.Models.Requests;
using ZLManage.DomainModel.Models.Responses;

namespace ZLManage.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdministratorController : ControllerBase
{
    private readonly IAdministratorService _service;
    public AdministratorController(IAdministratorService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AdministratorGetResponse>>> GetAll()
    {
        var list = await _service.GetAllAsync();
        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AdministratorGetResponse>> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<AdministratorGetResponse>> Create([FromBody] AdministratorCreateRequest request)
    {
        var created = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.AdminId }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] AdministratorUpdateRequest request)
    {
        if (id != request.AdminId) return BadRequest();
        var updated = await _service.UpdateAsync(request);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}