using FirstProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UsersController : ControllerBase
{
    
private readonly AppDbContext _context;

public UsersController(AppDbContext context)
    {
        _context = context;
    }

[HttpGet]

public IActionResult GetUsers()
    {
        var users = _context.Users.ToList();

        return Ok(users);
    }

[HttpGet("{id}")]

public IActionResult GetUser(int id)
    {
        var user = _context.Users.Find(id);

        if ( user == null )
        {
            return NotFound();
        }
        else 
        return Ok(user);
    }

[HttpPost]

public IActionResult CreateUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();

        return Created($"api/users/{user.Id}", user);
    }

}