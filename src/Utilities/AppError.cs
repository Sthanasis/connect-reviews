using connect.Reviews.Models;
using Microsoft.AspNetCore.Mvc;

namespace connect.Reviews.Utilities;

public class AppErrorUtility : ControllerBase
{
    public ActionResult SendServerError(string message)
    {
        return StatusCode(500, new AppResult()
        {
            Error = new ErrorViewModel { Message = message, Status = 500 },
        });
    }

    public ActionResult SendClientError(string message)
    {
        return NotFound(new AppResult()
        {
            Error = new ErrorViewModel { Message = message, Status = 404 },
        });
    }
}