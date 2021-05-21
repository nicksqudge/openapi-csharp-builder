using System;

namespace OpenApiBuilder
{
    public class StringFormatter
    {
        public static string ToCamelCase(string input)
        {
            return Char.ToLowerInvariant(input[0]) + input.Substring(1);
        }
    }
}
