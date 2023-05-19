using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers;

[ApiController]
public class IndexController : ControllerBase
{
    [HttpGet]
    [Route("/api")]
    public dynamic Get(string input)
    {
        return new {
            input = input
        };
    }

    [HttpPost]
    [Route("/api")]
    public dynamic Post([FromBody] Dictionary<string, string> input)
    {
        return new {
            input = input
        };
    }

    [HttpPut]
    [Route("/api")]
    public dynamic Put([FromBody] Dictionary<string, string> input)
    {
        return new {
            input = input
        };
    }

    [HttpDelete]
    [Route("/api")]
    public dynamic Delete(string input)
    {
        return new {
            input = input
        };
    }
}
