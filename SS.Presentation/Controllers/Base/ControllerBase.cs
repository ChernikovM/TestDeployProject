namespace SS.Presentation.Controllers.Base;

public class ControllerBase<TController> : Microsoft.AspNetCore.Mvc.ControllerBase
{
    protected ILogger<TController> Logger;
}