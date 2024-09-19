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
    private readonly Mock<ILogger<NameInfoController>> _mockLogger2;
    private readonly HttpClient _mockHttpClient;
    private readonly IbgeService _ibgeService;

    public IbgeServiceTests()
    {
        _mockLogger = new Mock<ILogger<IbgeService>>();
        _mockLogger2 = new Mock<ILogger<NameInfoController>>();
        _ibgeService = new IbgeService(_mockLogger.Object);
    }

    [Fact]
    public void GetNames_ReturnsOkResult_WithListOfNames()
    {
        // Arrange
        var controller = new NameInfoController(_mockLogger2.Object, _ibgeService);

        // Act
        var result = controller.GetNames() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        Assert.IsType<string[]>(result.Value);
        string[] names = result.Value as string[];
        Assert.Equal(NamesList.Names, names);
    }
}
