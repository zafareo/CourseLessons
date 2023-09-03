using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GapKo_p.Controllers
{
    //[Route("api/[controller]")]
   // [ApiController]
    public class ApiBaseController : Controller
    {
        protected IMediator _mediatr
        => HttpContext.RequestServices.GetRequiredService<IMediator>();

    }
}
