

using api.Constrollers;
using api.Dtos.Comment;
using api.Models;
using api.Repository;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NFluent;
using NSubstitute;

namespace TestApi
{

    public class CommentControllerTest
    {
        private readonly ICommentRepository _commentRepository;
        private readonly CommentController _commentController;
        private readonly Faker<Comment> _faker;

        public CommentControllerTest()
        {
            _commentRepository = Substitute.For<ICommentRepository>();
            _commentController = new CommentController(_commentRepository);
            _faker = new Faker<Comment>()
                 .RuleFor(c => c.Id, f => f.Random.Int(1, 100))
                 .RuleFor(c => c.Title, f => f.Lorem.Sentence())
                 .RuleFor(c => c.Content, f => f.Lorem.Paragraph())
                 .RuleFor(c => c.CreatedOn, f => f.Date.Past())
                 .RuleFor(c => c.StockId, f => f.Random.Int(1, 50));
        }

        public class GetAllCommentsTest : CommentControllerTest
        {

            [Fact]
            public async Task GetAllAsync_ShouldReturnAllValidComments_WhenValidComments()
            {
                // Arrange
                var comments = _faker.Generate(2);
                _commentRepository.GetAllAsync().Returns(Task.FromResult(comments));

                // Act
                var actual = await _commentController.GetAllAsync();

                // Assert
                Check.That(actual).IsInstanceOf<OkObjectResult>();
                var statusCodeResult = actual as IStatusCodeActionResult;
                Check.That(statusCodeResult.StatusCode).IsEqualTo(200);
                var actualObject = actual as OkObjectResult;
                Check.That(actualObject.Value).IsInstanceOf<List<CommentDtos>>();
                var value = actualObject.Value as List<CommentDtos>;
                Check.That(value).HasSize(2).And.ContainsOnlyInstanceOfType(typeof(CommentDtos));
                //Check.That(value).ContainsExactly(comments);

            }
            [Fact]
            public async Task GetAllAsync_ShouldReturnNoContent_WhenNoComments()
            {
                // Empty List ? Or Null ??
                var comments = new List<Comment>();
                _commentRepository.GetAllAsync().Returns(Task.FromResult(comments));

                // Act
                var actual = await _commentController.GetAllAsync();

                // Assert
                Check.That(actual).IsInstanceOf<NoContentResult>();
                var statusCodeResult = actual as IStatusCodeActionResult;
                Check.That(statusCodeResult.StatusCode).IsEqualTo(204);


            }
        }

        public class GetOneCommentTest : CommentControllerTest
        {
            [Theory]
            [InlineData(32)]
            [InlineData(33)]
            [InlineData(302)]

            public async Task GetById_ShouldReturnOneComment_WhenGivenGoodId(int id)
            {
                // Arrange
                var comment = _faker.RuleFor(c => c.Id, f => id).Generate();
                _commentRepository.GetByIdAsync(Arg.Any<int>()).Returns(Task.FromResult(comment));
                var controller = new CommentController(_commentRepository);

                // Act
                var actual = await controller.GetById(id);

                // Assert
                Check.That(actual).IsInstanceOf<ActionResult<CommentDtos>>();
                Check.That(actual.Result).IsInstanceOf<OkObjectResult>();
                var result = actual.Result as OkObjectResult;
                Check.That(result.StatusCode).IsEqualTo(200); // Response 200
                var value = result.Value as CommentDtos;
                Check.That(value.Id).IsEqualTo(comment.Id);
                Check.That(value.Title).IsEqualTo(comment.Title);
                Check.That(value.Content).IsEqualTo(comment.Content);
                Check.That(value.CreatedOn).IsEqualTo(comment.CreatedOn);
                Check.That(value.StockId).IsEqualTo(comment.StockId);
            }

            [Fact]
            public async Task GetById_ShouldThrowError_WhenGivenWrongId()
            {
                // Arrange
                _commentRepository.GetByIdAsync(Arg.Any<int>()).Returns(Task.FromResult<Comment>(null));
                var controller = new CommentController(_commentRepository);

                // Act
                var actual = await controller.GetById(999);

                // Assert
                Check.That(actual).IsInstanceOf<ActionResult<CommentDtos>>();
                Check.That(actual.Result).IsInstanceOf<NotFoundResult>();
                var result = actual.Result as NotFoundResult;
                Check.That(result.StatusCode).IsEqualTo(404);
            }

        }
    }
}