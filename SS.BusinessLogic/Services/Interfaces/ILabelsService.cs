using SS.BusinessLogic.Models.Base;
using SS.BusinessLogic.Models.Labels;

namespace SS.BusinessLogic.Services.Interfaces;

public interface ILabelsService
{
    Task<CollectionResponseBase<Label>> GetLabels(CollectionRequestBase request);
}