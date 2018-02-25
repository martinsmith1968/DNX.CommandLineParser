using System;
using System.Reflection;

namespace DNX.CommandLineParser.Options
{
    public interface IOptionDetails
    {
        OptionType OptionType { get; }

        Type PropertyType { get; }

        PropertyInfo PropertyInfo { get; }

        bool IsEnumerable { get; }

        bool Required { get; }

        object DefaultValue { get; }

        bool HasDefaultValue { get; }

        string ShortName { get; }

        string LongName { get; }

        string Name { get; }

        string Description { get; }

        int Position { get; }

        void SetValue(object instance, object value);
    }
}
