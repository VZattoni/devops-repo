using devops_project.Controllers;
using devops_project.Helpers;
using devops_project.Interfaces;
using devops_project.Models;
using devops_project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class IbgeServiceTests
{
    private readonly Mock<ILogger<IbgeService>> _mockLogger;
    private readonly Mock<ILogger<NameInfoController>> _loggerController;
    private readonly IbgeService _ibgeService;
    private readonly StaticNamesWrapper _staticNamesWrapper;
    private readonly NameInfoController _nameInfoController;

    public IbgeServiceTests()
    {
        _mockLogger = new Mock<ILogger<IbgeService>>();
        _loggerController = new Mock<ILogger<NameInfoController>>();
        _ibgeService = new IbgeService(_mockLogger.Object);
        _staticNamesWrapper = new StaticNamesWrapper();
        _nameInfoController = new NameInfoController(_loggerController.Object, _ibgeService, _staticNamesWrapper);
    }

    [Fact]
    public void GetNames_ReturnsOkResult_WithListOfNames()
    {
        // Act
        OkObjectResult? result = _nameInfoController.GetNames() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.IsType<string[]>(result.Value);
        string[]? names = result.Value as string[];
        Assert.Equal(_staticNamesWrapper.GetNamesList(), names);
    }


    [Fact]
    public void GetConcatNames_ReturnsOkResult_WithConcatenatedNames()
    {
        // Arrange
        string firstName = "John";
        string secondName = "Doe";

        // Act
        OkObjectResult? result = _nameInfoController.GetConcatNames(firstName, secondName) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.IsType<string>(result.Value);
        Assert.Equal("John Doe", result.Value);
    }

    [Theory]
    [InlineData(null, "Doe")]
    [InlineData("John", null)]
    [InlineData("", "Doe")]
    [InlineData("John", "")]
    public void GetConcatNames_ReturnsInternalServerError_WhenFirstOrSecondNameIsNullOrEmpty(string firstName, string secondName)
    {
        // Act
        var result = _nameInfoController.GetConcatNames(firstName, secondName) as StatusCodeResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }

    [Fact]
    public void GetName_ReturnsOkResult_WithRandomName()
    {
        // Arrange
        var namesList = _staticNamesWrapper.GetNamesList();

        // Act
        var result = _nameInfoController.GetName() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.IsType<string>(result.Value);

        // Check that the returned name is in the list
        var randomName = result.Value as string;
        Assert.Contains(randomName, namesList);
    }

    [Fact]
    public void GetName_ReturnsInternalServerError_WhenNamesListIsNull()
    {
        // Arrange
        Mock<IStaticNamesWrapper> mockNamesWrapper = new Mock<IStaticNamesWrapper>();
        mockNamesWrapper.Setup(wrapper => wrapper.GetNamesList()).Returns((string[]) null);

        NameInfoController controller = new NameInfoController(_loggerController.Object, _ibgeService, mockNamesWrapper.Object);

        // Act
        StatusCodeResult? result = controller.GetName() as StatusCodeResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}
