namespace SS.BusinessLogic.Models.Base;

public abstract class ResponseBase<TDataEntity> : ErrorResponseBase
{
    public TDataEntity Data { get; set; }
}