using System;
using System.Linq;

namespace OpenApiBuilder
{
    public class TypeIdentifier
    {
        public static string Integer = "integer";
        public static string Number = "number";
        public static string Array = "array";
        public static string String = "string";
        public static string Object = "object";
        public static string Boolean = "boolean";

        public static (string Type, string Format) Id(Type input)
        {
            if (IsArray(input))
                return (Array, null);

            string format = input.Name.ToLower();
            string typeName = GetType(format);

            if (SetFormatToEmpty(typeName))
                return (typeName, null);
                
            return (typeName, format);
        }

        public static string Name(Type inputType)
        {
            if (inputType.GenericTypeArguments.Any() == false)
                return inputType.Name;

            string result = string.Empty;
            
            foreach (var type in inputType.GenericTypeArguments)
                result += type.Name;

            result += inputType.Name.Replace($"`{inputType.GenericTypeArguments.Count()}", "");

            return result;   
        }

        private static bool SetFormatToEmpty(string typeName)
            => typeName == String || typeName == Object;

        private static bool IsArray(Type type)
        {
            if (type.IsArray)
                return true;

            if (type.Namespace.StartsWith("System.Collections."))
                return true;

            return false;
        }

        private static string GetType(string format)
        {
            switch (format)
            {
                case "int":
                case "int32":
                case "int64":
                    return Integer;

                case "float":
                case "double":
                    return Number;

                case "string":
                case "byte":
                case "binary":
                case "datetime":
                    return String;

                case "boolean":
                case "bool":
                    return Boolean;
            }

            return Object;
        }
    }
}
