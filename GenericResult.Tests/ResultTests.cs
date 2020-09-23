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
            var result = Result<Client>.Succeeds(client);

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
            var result = Result<Client>.Fails(message);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Null(result.Value);

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
            var result = Result<Client>.Fails(messages);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Null(result.Value);

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
            var result = Result<Client>.Fails();

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Null(result.Value);

            Assert.True(result.IsFailed);
            Assert.Empty(result.Errors);
            Assert.Null(result.Error);
        }

        [Fact]
        public void Succeeds_WithNoValue_ReturnsSuccessfulResult()
        {
            // Arrange

            // Act
            var result = Result<Client>.Succeeds();

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Null(result.Value);

            Assert.False(result.IsFailed);
            Assert.Empty(result.Errors);
            Assert.Null(result.Error);
        }
    }
}
