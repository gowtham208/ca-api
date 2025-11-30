// Controllers/UsersController.cs
using ca_api.Interfaces;
using ca_api.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateUserDto dto)
    {
        var userId = _userService.CreateUser(dto);
        return CreatedAtAction(nameof(GetById), new { id = userId }, null);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_userService.GetAllUsers());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        return Ok(_userService.GetUserById(id));
    }
}
