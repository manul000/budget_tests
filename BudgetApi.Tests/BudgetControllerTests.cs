using System.Globalization;
using System.Text.Json;
using BudgetApi.Controllers;
using BudgetApi.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BudgetApi.Tests;

public class BudgetControllerTests
{
    private readonly BudgetController controller = new();

    [Theory]
    [InlineData("100", 5, false, "500")]
    [InlineData("100", 5, true, "525")]
    [InlineData("0", 5, false, "0")]
    [InlineData("0", 5, true, "0")]
    [InlineData("100.50", 3, false, "301.50")]
    [InlineData("100.50", 3, true, "316.58")]
    [InlineData("0.10", 1, true, "0.11")]
    public void Calculate_ValidInput_ReturnsOkAndExpectedMoney(string daily, int days, bool insurance, string expected)
    {
        var request = new BudgetRequest
        {
            DailyCost = decimal.Parse(daily, CultureInfo.InvariantCulture),
            Days = days,
            IncludeInsurance = insurance
        };

        var response = Assert.IsType<OkObjectResult>(controller.Calculate(request));

        Assert.Equal(200, response.StatusCode);
        var body = JsonSerializer.SerializeToElement(response.Value);
        Assert.Equal(decimal.Parse(expected, CultureInfo.InvariantCulture), body.GetProperty("TotalAmount").GetDecimal());
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Calculate_NonPositiveDays_ReturnsBadRequest(int days)
    {
        var request = new BudgetRequest { DailyCost = 100, Days = days };

        var response = Assert.IsType<BadRequestObjectResult>(controller.Calculate(request));

        Assert.Equal(400, response.StatusCode);
        Assert.Equal("Количество дней должно быть больше нуля.", response.Value);
    }
}
