using Newtonsoft.Json;

namespace SS.BusinessLogic.Converters;

public class EnumConverter<TEnum> : JsonConverter<TEnum?>
    where TEnum : struct, Enum
{
    public override void WriteJson(JsonWriter writer, TEnum? value, JsonSerializer serializer)
    {
        if (value is not null)
        {
            writer.WriteValue(Enum.GetName(value.Value));
        }
    }

    public override TEnum? ReadJson(
        JsonReader reader, Type objectType, TEnum? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        TEnum? result = null;
        string value = reader.Value?.ToString();

        if (!string.IsNullOrEmpty(value))
            // ReSharper disable once ReplaceWithFirstOrDefault.1
            result = Enum.GetValues<TEnum>()
                .Any(enumValue => Enum.GetName(enumValue)?.Equals(value, StringComparison.InvariantCultureIgnoreCase) is true)
                ? Enum.GetValues<TEnum>()
                    .First(enumValue => Enum.GetName(enumValue)!.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                : null;

        return result;
    }
}