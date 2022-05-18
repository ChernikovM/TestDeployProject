namespace SS.Parser.Base;

public interface IDbUpdater
{
    Task<bool> UpdateAsync();
}