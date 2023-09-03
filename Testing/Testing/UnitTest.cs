using Moq;
using System.Text.RegularExpressions;
using Test;
using Test.Controllers;
using Test.Db;
using Xunit;

namespace Testing;

public class UnitTest
{
    [Fact]
    public async Task TestAsync()
    {
        var MockLogger = new Mock<ILogger<WeatherForecastController>>();
        var _db = new Mock<IDbContext>();
        var controller = new WeatherForecastController(MockLogger.Object, _db.Object);
        var res =  controller.Get();
        var forecasts = Assert.IsType<WeatherForecast[]>(res);
        Assert.Equal(5, forecasts.Length);
        foreach (var forecast in forecasts)
        {
            Assert.NotNull(forecast.Date);
            Assert.NotNull(forecast.Summary);
            Assert.InRange(forecast.TemperatureC, -20, 55);
        }          
    }
}


