using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using SigmaCandidate.Controllers;
using SigmaCandidate.Data;
using SigmaCandidate.Model;
using SigmaCandidate.Repository;

namespace SigmaCandidate.Test
{
    public class CandidateControllerTests
    {


        public class CandidatesControllerTests
        {
            private readonly Mock<ICandidateRepository> _mockRepository;
            private readonly CandidatesController _controller;

            public CandidatesControllerTests()
            {
                _mockRepository = new Mock<ICandidateRepository>();
                _controller = new CandidatesController(_mockRepository.Object);
            }



            [Fact]
            public async Task UpsertCandidate_ShouldReturnBadRequest_WhenEmailIsMissing()
            {
                var candidate = new Candidate
                {
                    FirstName = "John",
                    LastName = "Doe",
                };
                _controller.ModelState.AddModelError("Email", "The Email field is required.");
                var result = await _controller.UpsertCandidate(candidate);

                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                var errorResponse = Assert.IsType<ErrorResponse>(badRequestResult.Value);
                Assert.NotNull(errorResponse.Errors);
                Assert.Contains("The Email field is required.", errorResponse.Errors);
            }



            [Fact]
            public async Task UpsertCandidate_ShouldReturnBadRequest_WhenModelStateIsInvalid()
            {
                var candidate = new Candidate(); 
                _controller.ModelState.AddModelError("FirstName", "The FirstName field is required.");
                _controller.ModelState.AddModelError("LastName", "The LastName field is required.");
                _controller.ModelState.AddModelError("Email", "The Email field is required.");
                _controller.ModelState.AddModelError("Comment", "The Comment field is required.");

                var result = await _controller.UpsertCandidate(candidate);

                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                var errorResponse = Assert.IsType<ErrorResponse>(badRequestResult.Value);
                Assert.NotNull(errorResponse.Errors);
                Assert.Contains("The FirstName field is required.", errorResponse.Errors);
                Assert.Equal<int>(4, errorResponse.Errors.Count);
            }

            [Fact]
            public async Task UpsertCandidate_ShouldUpdateCandidate_WhenCandidateExists()
            {
                var candidate = new Candidate
                {
                    Email = "john@doe.com",
                    FirstName = "John",
                    LastName = "Doe",
                    Comment = "HI I am john doe."

                };

                _mockRepository.Setup(repo => repo.GetCandidateByEmailAsync(candidate.Email))
                    .ReturnsAsync(new Candidate { Email = candidate.Email });

                var result = await _controller.UpsertCandidate(candidate);

                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnedCandidate = Assert.IsType<Candidate>(okResult.Value);
                Assert.Equal(candidate.Email, returnedCandidate.Email);
                _mockRepository.Verify(repo => repo.UpdateCandidate(candidate), Times.Once);
                _mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
            }

            [Fact]
            public async Task UpsertCandidate_ShouldCreateCandidate_WhenCandidateDoesNotExist()
            {
                var candidate = new Candidate
                {
                    Email = "john@doe.com",
                    FirstName = "John",
                    LastName = "Doe",
                    Comment = "HI I am john doe."
                };

                _mockRepository.Setup(repo => repo.GetCandidateByEmailAsync(candidate.Email))
                    .ReturnsAsync(default (Candidate));

                var result = await _controller.UpsertCandidate(candidate);

                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnedCandidate = Assert.IsType<Candidate>(okResult.Value);
                Assert.Equal(candidate.Email, returnedCandidate.Email);

                _mockRepository.Verify(repo => repo.CreateCandidateAsync(candidate), Times.Once);
                _mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
            }

            [Fact]
            public async Task UpsertCandidate_ShouldReturnInternalServerError_WhenExceptionIsThrown()
            {
                var candidate = new Candidate
                {
                    Email = "john@doe.com",
                    FirstName = "John",
                    LastName = "Doe",
                    Comment = "HI I am john doe."
                };

                _mockRepository.Setup(repo => repo.GetCandidateByEmailAsync(It.IsAny<string>()))
                    .ThrowsAsync(new Exception("Database error"));

                var result = await _controller.UpsertCandidate(candidate);

                var statusCodeResult = Assert.IsType<ObjectResult>(result);
                Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
                Assert.Equal("An error occurred while processing your request.", statusCodeResult.Value);
            }
        }
    }
}