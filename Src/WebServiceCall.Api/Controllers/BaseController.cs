using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceCall.Api.Controllers
{
    [Route("[Controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class BaseController : Controller
    {
    }
}