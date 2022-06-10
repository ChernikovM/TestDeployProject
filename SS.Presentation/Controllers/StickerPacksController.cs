using Microsoft.AspNetCore.Mvc;
using SS.BusinessLogic.Models.Base;
using SS.BusinessLogic.Services.Interfaces;

namespace SS.Presentation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class StickerPacksController : Base.ControllerBase<StickerPacksController>
{
    private readonly IStickersService _stickersService;

    public StickerPacksController(
        ILogger<StickerPacksController> logger, IStickersService stickersService)
    {
        this.Logger = logger;
        this._stickersService = stickersService;
    }

    [HttpPost]
    public async Task<IActionResult> Get([FromBody] CollectionRequestBase request)
    {
        return new OkObjectResult(await this._stickersService.GetStickerPacks(request));
    }
}