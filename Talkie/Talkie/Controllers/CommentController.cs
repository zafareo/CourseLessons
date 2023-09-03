using Application.MediatrEntities.Comments.Commands;
using Application.MediatrEntities.Comments.Queries;
using Microsoft.AspNetCore.Mvc;

namespace GapKo_p.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ApiBaseController
    {
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateComment([FromForm] CreateCommentCommand command)
        => Ok(await _mediatr.Send(command));

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> DeleteComment([FromForm] DeleteCommentCommand command)
            => Ok(await _mediatr.Send(command));


        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> UpdateComment([FromForm] UpdateCommentCommand command)
            => Ok(await _mediatr.Send(command));


        [HttpGet("getall")]
        public async Task<IActionResult> GetAllComment()
        => Ok(await _mediatr.Send(new GetAllComentQuery()));

        [HttpGet("getById")]
        public async Task<IActionResult> GetByIdComment([FromQuery] GetByIdComentQuery command)
            => Ok(await _mediatr.Send(command));
    }
}
