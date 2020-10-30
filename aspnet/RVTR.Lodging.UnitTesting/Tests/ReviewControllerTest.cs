using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using RVTR.Lodging.ObjectModel.Interfaces;
using RVTR.Lodging.ObjectModel.Models;
using RVTR.Lodging.WebApi.Controllers;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class ReviewControllerTest
  {
    private readonly ReviewController _controller;
    private readonly ILogger<ReviewController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ReviewControllerTest()
    {
      var loggerMock = new Mock<ILogger<ReviewController>>();
      var repositoryMock = new Mock<IRepository<ReviewModel>>();
      var unitOfWorkMock = new Mock<IUnitOfWork>();

      repositoryMock.Setup(m => m.DeleteAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.DeleteAsync(1)).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.InsertAsync(It.IsAny<ReviewModel>())).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.SelectAsync()).ReturnsAsync((IEnumerable<ReviewModel>)null);
      repositoryMock.Setup(m => m.SelectAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.SelectAsync(1)).ReturnsAsync((ReviewModel)null);
      repositoryMock.Setup(m => m.SelectAsync(2)).ReturnsAsync(new ReviewModel() { Id = 2, Comment = "Random", DateCreated = DateTime.Now, Rating = 1 });
      repositoryMock.Setup(m => m.Update(It.IsAny<ReviewModel>()));
      unitOfWorkMock.Setup(m => m.Review).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new ReviewController(_logger, _unitOfWork);
    }

    [Fact]
    public async void Test_Controller_Delete()
    {
      var resultFail = await _controller.Delete(0);
      var resultPass = await _controller.Delete(1);

      Assert.NotNull(resultFail);
      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Get()
    {
      var resultMany = await _controller.Get();
      var resultFail = await _controller.Get(0);
      var resultOne = await _controller.Get(1);

      Assert.NotNull(resultMany);
      Assert.NotNull(resultFail);
      Assert.NotNull(resultOne);
    }

    [Fact]
    public async void Test_Controller_Post()
    {
      var resultPass = await _controller.Post(new ReviewModel());

      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put()
    {
      ReviewModel reviewmodel = await _unitOfWork.Review.SelectAsync(2);

      var resultPass = await _controller.Put(reviewmodel);
      var resultFail = await _controller.Put(null);

      Assert.NotNull(resultPass);
      Assert.NotNull(resultFail);
    }
  }
}
