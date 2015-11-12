using System;
using System.ComponentModel;

namespace GreenMachine.Common.Extensions
{
    /// <summary>
    /// Extension methods for serializing and deserializing Enum objects to Strings.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Deserialize the given Enum value to a String representation. A [Description] tag is required for this operation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The string representation of the enum value.</returns>
        public static string ToDescription(this Enum value)
        {
            var da = (DescriptionAttribute[])
                (value.GetType().GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false));

            return da.Length > 0 ? da[0].Description : value.ToString();
        }

        /// <summary>
        /// Convert the given string value into the Enum value.
        /// </summary>
        /// <param name="stringValue">The string you are trying to convert.</param>
        /// <param name="defaultValue">The default Enum value if conversion fails.</param>
        /// <returns>An Enum reprsentation of the string.</returns>
        public static T ToEnum<T>(this string stringValue, T defaultValue)
        {
            foreach (T enumValue in Enum.GetValues(typeof(T)))
            {
                var da = (DescriptionAttribute[])
                    (typeof(T).GetField(enumValue.ToString())
                    .GetCustomAttributes(typeof(DescriptionAttribute), false));

                if (da.Length > 0 && da[0].Description == stringValue)
                    return enumValue;
            }

            return defaultValue;
        }
    }
}
