namespace SS.BusinessLogic.Models.Base;

public abstract class ErrorResponseBase
{
    public List<string> Errors { get; }

    public ErrorResponseBase()
    {
    }

    public ErrorResponseBase(Exception ex)
    {
        (this.Errors = new List<string>()).Add(ex.Message);
    }
}