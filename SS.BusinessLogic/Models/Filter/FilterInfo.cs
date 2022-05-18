using Newtonsoft.Json;
using SS.BusinessLogic.Converters;

namespace SS.BusinessLogic.Models.Filter;

public class FilterInfo
{
    public string FieldName { get; set; }

    [JsonConverter(typeof(EnumConverter<FilterOption>))]
    public FilterOption? Option { get; set; }

    public string Value { get; set; }
}