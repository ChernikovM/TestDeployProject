using SS.BusinessLogic.Models.Base;
using SS.BusinessLogic.Models.Filter;
using SS.BusinessLogic.Models.Labels;
using SS.BusinessLogic.Models.Page;
using SS.BusinessLogic.Providers;
using SS.BusinessLogic.Services.Interfaces;
using SS.DataAccess.Repositories.EF.Interfaces;

namespace SS.BusinessLogic.Services;

public class LabelsService : ILabelsService
{
    private readonly ILabelRepository _labelRepository;

    public LabelsService(ILabelRepository labelRepository)
    {
        this._labelRepository = labelRepository;
    }

    public async Task<CollectionResponseBase<Label>> GetLabels(CollectionRequestBase request)
    {
        CollectionResponseBase<Label> response = new CollectionResponseBase<Label>();

        Func<DataAccess.Entities.Label, bool> expression =
            FilteringProvider.BuildFilter<DataAccess.Entities.Label>(request?.Filters, out List<FilterInfo> validFilters);

        List<DataAccess.Entities.Label> filteredLabels = expression is not null
            ? (await this._labelRepository.FindAsync(expression)).ToList()
            : (await this._labelRepository.GetAllAsync()).ToList();

        response.Data = CollectionDataAccessProvider
            .PaginateData(filteredLabels, request, out PageInfoResponse pageInfo)
            .Select(this.MapLabel)
            .ToList();

        response.Filters = validFilters;
        response.PageInfo = pageInfo;

        return response;
    }

    private Label MapLabel(DataAccess.Entities.Label entryEntity)
    {
        Label label = new Label(entryEntity);

        return label;
    }
}