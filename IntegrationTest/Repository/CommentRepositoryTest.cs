using api.Data;
using api.Models;
using api.Repository;
using Microsoft.EntityFrameworkCore;
using NFluent;
using Testcontainers.MsSql;

namespace IntegrationTest
{
    public class CommentRepositoryTests
    {
        private ICommentRepository _repository;

        public CommentRepositoryTests()
        {
         
            _repository = new CommentRepository(dbContext);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoCommentsExist()
        {
            // Act
            var comments = await _repository.GetAllAsync();

            // Assert
            Check.That(comments).IsNotNull();
            Check.That(comments).HasSize(1);  
            Check.That(comments.First().Content).IsEqualTo("Test comment");
        }
    }
}
