using SS.DataAccess.Entities;
using SS.DataAccess.Repositories.EF.Base;
using SS.DataAccess.Repositories.EF.Interfaces;

namespace SS.DataAccess.Repositories.EF;

public class LabelRepository : EFRepository<Label>, ILabelRepository
{
    public LabelRepository(AppContext context) : base(context)
    {
    }
}