using BudgetApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApi.Controllers;

[ApiController]
[Route("api/budget")]
public class BudgetController : ControllerBase
{
    [HttpPost("calculate")]
    public IActionResult Calculate([FromBody] BudgetRequest request)
    {
        if (request.Days <= 0)
            return BadRequest("Количество дней должно быть больше нуля.");

        var total = request.DailyCost * request.Days;
        if (request.IncludeInsurance)
            total *= 1.05m;

        // Monetary results are rounded to kopecks, with midpoint values away from zero.
        total = decimal.Round(total, 2, MidpointRounding.AwayFromZero);
        return Ok(new { TotalAmount = total });
    }
}
