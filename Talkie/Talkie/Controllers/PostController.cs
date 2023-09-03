using Application.MediatrEntities.Comments.Queries;
using Application.MediatrEntities.Posts.Commands;
using Application.MediatrEntities.Posts.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GapKo_p.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ApiBaseController
    {
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostCommand command)
       => Ok(await _mediatr.Send(command));

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> DeletePost([FromForm] DeletePostCommand command)
            => Ok(await _mediatr.Send(command));


        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> UpdatePost([FromForm] UpdatePostCommand command)
            => Ok(await _mediatr.Send(command));


        [HttpGet("getall")]
        public async Task<IActionResult> GetAllPost()
        => Ok(await _mediatr.Send(new GetAllPostsQuery()));

        [HttpGet("getById")]
        public async Task<IActionResult> GetByIdPost([FromQuery] GetByIdComentQuery command)
            => Ok(await _mediatr.Send(command));
    }
}
