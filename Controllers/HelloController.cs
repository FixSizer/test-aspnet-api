using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloController : ControllerBase
{
    
    [HttpGet]
    public string Get()
    {
        return "Hello pidoras tupoy";
    }

}