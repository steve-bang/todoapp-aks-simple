
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Services;

namespace ToDoApi.Apis;

[ApiController]
[Route("api/todos")]
public class TodoApi : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoApi(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpPost]
    public IActionResult Create([FromBody]Todo data)
    {
        return Ok(_todoService.Create(data));
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_todoService.GetAll());
    }

    [HttpPost("{id}/completed")]
    public IActionResult MarkCompleted(Guid id)
    {
        return Ok(_todoService.MarkCompleted(id));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        return Ok(_todoService.Delete(id));
    }

}