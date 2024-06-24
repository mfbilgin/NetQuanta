using Business.Constants.Messages;
using Core.Logging;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogsController(ILogService logService) : ControllerBase
{
    [HttpDelete]
    public IActionResult Delete([FromQuery] Guid id)
    {
        logService.DeleteLog(id);
        return Ok(LogMessages.LogHasBeenDeleted);
    }
       
    [HttpDelete("delete-range")]
    public IActionResult DeleteRange([FromBody] List<Guid> ids)
    {
        logService.DeleteLogRange(ids);
        return Ok(LogMessages.LogsHasBeenDeleted);
    }
    
    [HttpDelete("delete-all")]
    public IActionResult DeleteAll()
    {
        logService.DeleteAllLogs();
        return Ok(LogMessages.LogsHasBeenDeleted);
    }
    
    
    [HttpGet]
    public IActionResult GetAll([FromQuery] int index = 1, [FromQuery] int size = 10)
    {
        var logs = logService.GetAllLogs(index, size);
        return Ok(logs);
    }
    
    [HttpGet("requestedValue/{requestedValue}")]
    public IActionResult GetByUsername(string requestedValue, [FromQuery] int index = 1, [FromQuery] int size = 10)
    {
        var logs = logService.GetLogsByRequestedValue(requestedValue, index, size);
        return Ok(logs);
    }
    
    [HttpGet("level/{logLevel}")]
    public IActionResult GetByLogLevel(string logLevel, [FromQuery] int index = 1, [FromQuery] int size = 10)
    {
        var logs = logService.GetLogsByLogLevel(logLevel, index, size);
        return Ok(logs);
    }
    
    [HttpGet("exception/{exception}")]
    public IActionResult GetByException(string exception, [FromQuery] int index = 1, [FromQuery] int size = 10)
    {
        var logs = logService.GetLogsByException(exception, index, size);
        return Ok(logs);
    }
    
}