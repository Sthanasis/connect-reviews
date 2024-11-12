using connect.Reviews.Models;
using connect.Reviews.Services;
using connect.Reviews.Utilities;

using Microsoft.AspNetCore.Mvc;
namespace connect.Reviews.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController(ReviewService reviewService) : ControllerBase
{
    private readonly ReviewService _reviewService = reviewService;

    private readonly AppErrorUtility _appErrorUtils = new();

    [HttpGet("product/{id:length(24)}")]
    public async Task<ActionResult<AppResult<List<Review>>>> Get(string id)
    {
        try
        {
            List<Review> reviews = await _reviewService.GetByProductId(id);
            AppResult<List<Review>> result = new() { Data = reviews };
            return Ok(result);
        }
        catch (Exception ex)
        {
            return _appErrorUtils.SendServerError(ex.Message);
        }
    }

    [HttpGet("user/{id:length(24)}")]
    public async Task<ActionResult<AppResult<Review>>> Get(int id)
    {
        try
        {
            List<Review> reviews = await _reviewService.GetByUserId(id);
            AppResult<List<Review>> result = new() { Data = reviews };
            return Ok(result);
        }
        catch (Exception ex)
        {
            return _appErrorUtils.SendServerError(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(Review newReview)
    {
        try
        {
            await _reviewService.CreateAsync(newReview);
            return CreatedAtAction(nameof(Get), new { id = newReview.Id }, newReview);
        }
        catch (Exception ex)
        {
            return _appErrorUtils.SendServerError(ex.Message);
        }
    }
}