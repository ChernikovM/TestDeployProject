namespace SS.BusinessLogic.Models.Labels;

public class Label
{
    public long Id { get; set; }

    public string Name { get; set; }

    public bool? IsRemoved { get; set; }

    public Label(DataAccess.Entities.Label label)
    {
        this.Id = label.Id;
        this.Name = label.Name;
        this.IsRemoved = label.IsRemoved;
    }
}