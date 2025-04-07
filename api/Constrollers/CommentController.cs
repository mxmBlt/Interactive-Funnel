using api.Dtos.Comment;
using api.Mappers;
using api.Repository;
using Microsoft.AspNetCore.Mvc;



namespace api.Constrollers
{
    [Route("api/comment")]
    [ApiController]

    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var comments = await _commentRepository.GetAllAsync();

            var commentDtos = comments.Select(c => c.ToCommentDto()).ToList();
            return commentDtos.Any() ? Ok(commentDtos) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDtos>> GetById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }
    }
}
