using Microsoft.AspNetCore.Mvc;
using SS.BusinessLogic.Models.Base;
using SS.BusinessLogic.Services.Interfaces;

namespace SS.Presentation.Controllers;

[ApiController]
[Route("api/labels")]
public class LabelsController : Base.ControllerBase<LabelsController>
{
    private readonly ILabelsService _labelsService;

    public LabelsController(
        ILogger<LabelsController> logger, ILabelsService labelsService)
    {
        this.Logger = logger;
        this._labelsService = labelsService;
    }

    [HttpPost("get")]
    public async Task<IActionResult> Get([FromBody] CollectionRequestBase request)
    {
        return new OkObjectResult(await this._labelsService.GetLabels(request));
    }
}