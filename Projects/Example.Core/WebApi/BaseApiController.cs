using Microsoft.AspNetCore.Mvc;

namespace Example.Core.WebApi
{
    
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [BodyContentType("application/json")]
    public abstract class BaseApiController : Controller
    {
    }
}