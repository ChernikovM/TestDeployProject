using System.Reflection;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SS.BusinessLogic.Models.Filter;

namespace SS.BusinessLogic.Providers;

public static class FilteringProvider
{
    public static Func<TEntity, bool> BuildFilter<TEntity>(List<FilterInfo> filters, out List<FilterInfo> validFilters)
    {
        validFilters = new List<FilterInfo>();
        Func<TEntity, bool> resultFilter = null;
        List<Predicate<TEntity>> predicates = new List<Predicate<TEntity>>();

        if (filters?.Any() is true)
        {
            foreach (FilterInfo filter in filters)
            {
                Predicate<TEntity> predicate = null;

                if (filter is not null &&
                    !string.IsNullOrEmpty(filter.Value) &&
                    !string.IsNullOrEmpty(filter.FieldName) &&
                    filter.Option is not null)
                {
                    PropertyInfo property = typeof(TEntity)
                        .GetProperty(filter.FieldName, BindingFlags.Instance | BindingFlags.Public);

                    if (property is not null &&
                        IsValidOption(filter.Option.Value, property))
                    {
                        predicate = CreateFilter<TEntity>(property, filter.Option.Value, filter.Value);
                    }

                    if (predicate is not null)
                    {
                        predicates.Add(predicate);
                        validFilters.Add(filter);
                    }
                }
            }

            if (predicates.Any())
            {
                Predicate<TEntity> mainPredicate = null;

                foreach (Predicate<TEntity> predicate in predicates)
                {
                    if (mainPredicate is null)
                    {
                        mainPredicate = predicate;
                    }
                    else
                    {
                        Predicate<TEntity> temp = mainPredicate;
                        mainPredicate = entity => temp(entity) && predicate(entity);
                    }
                }

                if (mainPredicate is not null)
                {
                    resultFilter = new Func<TEntity, bool>(mainPredicate);
                }
            }
        }

        return resultFilter;
    }

    private static bool IsValidOption(FilterOption option, PropertyInfo propertyInfo)
    {
        Type propertyType = propertyInfo.PropertyType;

        return AllowedTypes.TryGetValue(option, out List<Type> types) &&
               types.Contains(propertyType);
    }

    private static Predicate<TEntity> CreateFilter<TEntity>(
        PropertyInfo propertyInfo, FilterOption option, string value)
    {
        Predicate<TEntity> predicate = null;

        Type propertyType = propertyInfo.PropertyType;

        if (propertyType == typeof(string))
        {
            switch (option)
            {
                case FilterOption.Equals:
                    predicate =
                        entity =>
                            (propertyInfo.GetValue(entity) as string)
                            ?.Equals(value, StringComparison.InvariantCultureIgnoreCase)
                            is true;

                    break;

                case FilterOption.Contains:
                    predicate =
                        entity =>
                            (propertyInfo.GetValue(entity) as string)
                            ?.Contains(value, StringComparison.InvariantCultureIgnoreCase)
                            is true;

                    break;

                case FilterOption.NotContains:
                    predicate =
                        entity =>
                            (propertyInfo.GetValue(entity) as string)
                            ?.Contains(value, StringComparison.InvariantCultureIgnoreCase)
                            is false;

                    break;
            }
        }
        else if (IntegerTypes.Contains(propertyType))
        {
            long? filterValue = null;

            if (!value.Equals("null", StringComparison.InvariantCultureIgnoreCase) &&
                long.TryParse(value, out long parsed))
            {
                filterValue = parsed;
            }

            ValueConverter castingConverter = new CastingConverter<long?, long?>();

            if (propertyInfo.PropertyType == typeof(uint) ||
                propertyInfo.PropertyType == typeof(uint?))
            {
                castingConverter = new CastingConverter<uint?, long?>();
            }
            else if (propertyInfo.PropertyType == typeof(int) ||
                     propertyInfo.PropertyType == typeof(int?))
            {
                castingConverter = new CastingConverter<int?, long?>();
            }
            else if (propertyInfo.PropertyType == typeof(short) ||
                     propertyInfo.PropertyType == typeof(short?))
            {
                castingConverter = new CastingConverter<short?, long?>();
            }
            else if (propertyInfo.PropertyType == typeof(byte) ||
                     propertyInfo.PropertyType == typeof(byte?))
            {
                castingConverter = new CastingConverter<byte?, long?>();
            }
            else if (propertyInfo.PropertyType == typeof(sbyte) ||
                     propertyInfo.PropertyType == typeof(sbyte?))
            {
                castingConverter = new CastingConverter<sbyte?, long?>();
            }
            else if (propertyInfo.PropertyType == typeof(ushort) ||
                     propertyInfo.PropertyType == typeof(ushort?))
            {
                castingConverter = new CastingConverter<ushort?, long?>();
            }

            switch (option)
            {
                case FilterOption.Equals:
                    predicate =
                        entity =>
                            (long?) castingConverter.ConvertToProvider(propertyInfo.GetValue(entity)) == filterValue;

                    break;

                case FilterOption.GreaterOrEquals:
                    predicate =
                        entity =>
                            (long?) castingConverter.ConvertToProvider(propertyInfo.GetValue(entity)) >= filterValue;

                    break;

                case FilterOption.LessOrEquals:
                    predicate =
                        entity =>
                            (long?) castingConverter.ConvertToProvider(propertyInfo.GetValue(entity)) <= filterValue;

                    break;
            }
        }
        else if (FloatingPointTypes.Contains(propertyType))
        {
            decimal? filterValue = null;

            if (!value.Equals("null", StringComparison.InvariantCultureIgnoreCase) &&
                decimal.TryParse(value, out decimal parsed))
            {
                filterValue = parsed;
            }

            ValueConverter castingConverter = new CastingConverter<decimal, decimal?>();

            if (propertyInfo.PropertyType == typeof(float) ||
                propertyInfo.PropertyType == typeof(float?))
            {
                castingConverter = new CastingConverter<float, decimal?>();
            }
            else if (propertyInfo.PropertyType == typeof(double) ||
                     propertyInfo.PropertyType == typeof(double?))
            {
                castingConverter = new CastingConverter<double, decimal?>();
            }

            switch (option)
            {
                case FilterOption.Equals:
                    predicate =
                        entity =>
                            // (decimal?) propertyInfo.GetValue(entity) == filterValue;
                            (decimal?) castingConverter.ConvertToProvider(propertyInfo.GetValue(entity)) == filterValue;

                    break;

                case FilterOption.GreaterOrEquals:
                    predicate =
                        entity =>
                            (decimal?) castingConverter.ConvertToProvider(propertyInfo.GetValue(entity)) >= filterValue;

                    break;

                case FilterOption.LessOrEquals:
                    predicate =
                        entity =>
                            (decimal?) castingConverter.ConvertToProvider(propertyInfo.GetValue(entity)) <= filterValue;

                    break;
            }
        }
        else if (propertyType == typeof(bool) ||
                 propertyType == typeof(bool?))
        {
            bool? filterValue = null;

            if (!value.Equals("null", StringComparison.InvariantCultureIgnoreCase) &&
                bool.TryParse(value, out bool parsed))
            {
                filterValue = parsed;
            }

            switch (option)
            {
                case FilterOption.Equals:
                    predicate =
                        entity => (bool?) propertyInfo.GetValue(entity) == filterValue;

                    break;
            }
        }
        else if (propertyType == typeof(char) ||
                 propertyType == typeof(char?))
        {
            char? filterValue = null;

            if (!value.Equals("null", StringComparison.InvariantCultureIgnoreCase) &&
                char.TryParse(value, out char parsed))
            {
                filterValue = parsed;
            }

            switch (option)
            {
                case FilterOption.Equals:
                    predicate =
                        entity =>
                            (char?) propertyInfo.GetValue(entity) == filterValue;

                    break;
            }
        }
        else if (propertyType == typeof(DateTime) ||
                 propertyType == typeof(DateTime?))
        {
            DateTime? filterValue = null;

            if (!value.Equals("null", StringComparison.InvariantCultureIgnoreCase) &&
                DateTime.TryParse(value, out DateTime parsed))
            {
                filterValue = parsed;
            }

            if (filterValue is not null)
            {
                switch (option)
                {
                    case FilterOption.GreaterOrEquals:
                        predicate =
                            entity =>
                                (DateTime?) propertyInfo.GetValue(entity) >= filterValue;

                        break;

                    case FilterOption.LessOrEquals:
                        predicate =
                            entity =>
                                (DateTime?) propertyInfo.GetValue(entity) <= filterValue;
                                // DateTime.TryParse(propertyInfo.GetValue(entity) as string, out DateTime propertyDateTimeValue) &&
                                // DateTime.TryParse(value, out DateTime filterDateTimeValue) &&
                                // propertyDateTimeValue <= filterDateTimeValue;

                        break;
                }
            }
        }

        return predicate;
    }

    private static readonly List<Type> IntegerTypes = new()
    {
        typeof(int),
        typeof(int?),
        typeof(uint),
        typeof(uint?),
        typeof(long),
        typeof(long?),
        typeof(ulong),
        typeof(ulong?),
        typeof(short),
        typeof(short?),
        typeof(ushort),
        typeof(ushort?),
        typeof(byte),
        typeof(byte?),
        typeof(sbyte),
        typeof(sbyte?)
    };

    private static readonly List<Type> FloatingPointTypes = new()
    {
        typeof(float),
        typeof(float?),
        typeof(decimal),
        typeof(decimal?),
        typeof(double),
        typeof(double?)
    };

    private static readonly List<Type> NumberTypes = IntegerTypes.Union(FloatingPointTypes).ToList();

    private static readonly Dictionary<FilterOption, List<Type>> AllowedTypes = new()
    {
        {
            FilterOption.Equals,
            new List<Type>(NumberTypes)
            {
                typeof(string),
                typeof(char),
                typeof(char?),
                typeof(bool),
                typeof(bool?)
            }
        },
        {
            FilterOption.GreaterOrEquals,
            new List<Type>(NumberTypes)
            {
                typeof(DateTime),
                typeof(DateTime?)
            }
        },
        {
            FilterOption.LessOrEquals,
            new List<Type>(NumberTypes)
            {
                typeof(DateTime),
                typeof(DateTime?)
            }
        },
        {
            FilterOption.Contains,
            new List<Type>
            {
                typeof(string)
            }
        },
        {
            FilterOption.NotContains,
            new List<Type>
            {
                typeof(string)
            }
        }
    };
}