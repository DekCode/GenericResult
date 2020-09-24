using GenericResult.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GenericResult.Tests
{
    public class ResultTests
    {
        [Fact]
        public void Suceeds_WithValue_ReturnsSuccessfulResult()
        {
            // Arrange
            var client = new Client
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                ContactInformation = new Contact
                {
                    Phone = Guid.NewGuid().ToString(),
                    Address = Guid.NewGuid().ToString(),
                    Email = Guid.NewGuid().ToString(),
                    Mobile = Guid.NewGuid().ToString(),
                }
            };

            // Act
            var result = Result.Succeed(client);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Same(client, result.Value);

            Assert.False(result.IsFailed);
            Assert.Empty(result.Errors);
            Assert.Null(result.Error);
        }

        [Fact]
        public void Fails_WithSingleError_ReturnsFailedResult()
        {
            // Arrange
            var message = Guid.NewGuid().ToString();

            // Act
            var result = Result.Fail(message);

            // Assert
            Assert.False(result.IsSuccessful);

            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Same(message, result.Error);
        }

        [Fact]
        public void Fails_WithMultipleErrors_ReturnsFailedResult()
        {
            // Arrange
            var messages = new List<string>();
            for (var count = 0; count < 100; count++)
            {
                messages.Add(Guid.NewGuid().ToString());
            }

            // Act
            var result = Result.Fail(messages);

            // Assert
            Assert.False(result.IsSuccessful);

            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Equal<string>(messages, result.Errors);
            Assert.Same(messages.First(), result.Errors.First());
        }

        [Fact]
        public void Fails_WithNoError_ReturnsFailedResult()
        {
            // Arrange

            // Act
            var result = Result.Fail();

            // Assert
            Assert.False(result.IsSuccessful);

            Assert.True(result.IsFailed);
            Assert.Empty(result.Errors);
            Assert.Null(result.Error);
        }

        [Fact]
        public void Succeeds_WithNoValue_ReturnsSuccessfulResult()
        {
            // Arrange

            // Act
            var result = Result.Succeed();

            // Assert
            Assert.True(result.IsSuccessful);

            Assert.False(result.IsFailed);
            Assert.Empty(result.Errors);
            Assert.Null(result.Error);
        }

        [Fact]
        public void Fail_Integrity()
        {
            // Void result
            Assert.False(Result.Fail().IsSuccessful);
            Assert.False(Result.Fail("message").IsSuccessful);
            Assert.False(Result.Fail(new List<string> { "message1", "message2" }).IsSuccessful);

            // Return result
            Assert.False(Result.Fail<Client>().IsSuccessful);
            Assert.False(Result.Fail<Client>("message").IsSuccessful);
            Assert.False(Result.Fail<Client>(new List<string> { "message1", "message2" }).IsSuccessful);

            Assert.True(Result.Fail().IsFailed);
            Assert.True(Result.Fail("message").IsFailed);
            Assert.True(Result.Fail(new List<string> { "message1", "message2" }).IsFailed);

            // Return result
            Assert.True(Result.Fail<Client>().IsFailed);
            Assert.True(Result.Fail<Client>("message").IsFailed);
            Assert.True(Result.Fail<Client>(new List<string> { "message1", "message2" }).IsFailed);
        }

        [Fact]
        public void Suceed_Integrity()
        {
            // Void result
            Assert.True(Result.Succeed().IsSuccessful);
            Assert.True(Result.Succeed("message").IsSuccessful);
            Assert.True(Result.Succeed(new List<string> { "message1", "message2" }).IsSuccessful);

            // Return result
            Assert.True(Result.Succeed((Client)null).IsSuccessful);
            Assert.True(Result.Succeed(new Client()).IsSuccessful);

            // Void result
            Assert.False(Result.Succeed().IsFailed);
            Assert.False(Result.Succeed("message").IsFailed);
            Assert.False(Result.Succeed(new List<string> { "message1", "message2" }).IsFailed);

            // Return result
            Assert.False(Result.Succeed((Client)null).IsFailed);
            Assert.False(Result.Succeed(new Client()).IsFailed);
        }
    }
}
