using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Test
{
    static class TestHelper
    {
        static object ConvertSingleItem(object value, Type newType)
        {
            if (typeof(IConvertible).IsAssignableFrom(newType))
            {
                return Convert.ChangeType(value, newType);
            }
            return value;
        }

        static object ConvertStringToNewNonNullableType(object value, Type newType)
        {
            // Do conversion form string to array - not sure how array will be stored in string
            if (newType.IsArray)
            {
                // For comma separated list
                var singleItemType = newType.GetElementType();

                var elements = new ArrayList();
                foreach (
                    var convertedSingleItem in
                        value.ToString().Split(',').Select(element => ConvertSingleItem(element, singleItemType)))
                    elements.Add(convertedSingleItem);
                return elements.ToArray(singleItemType);
            }
            return ConvertSingleItem(value, newType);
        }

        static object ConvertStringToNewType(object value, Type newType)
        {
            // If it's not a nullable type, just pass through the parameters to Convert.ChangeType
            if (newType.IsGenericType && newType.GetGenericTypeDefinition() == typeof(Nullable<>))
                return value == null ? null : ConvertStringToNewNonNullableType(value, new NullableConverter(newType).UnderlyingType);
            return ConvertStringToNewNonNullableType(value, newType);
        }

        public static object CallMethod(object instance, MethodInfo methodInfo, List<object> parameters)
        {
            var methodParameters = methodInfo.GetParameters();

            var parametersForInvocation = new List<object>();
            for (var index = 0; index < methodParameters.Length; index++)
            {
                var methodParameter = methodParameters[index];
                var value = parameters[index];

                var convertedValue = ConvertStringToNewType(value, methodParameter.ParameterType);
                parametersForInvocation.Add(convertedValue);
                //else
                //{
                //    // Get default value of the appropriate type or throw an exception
                //    var defaultValue = Activator.CreateInstance(methodParameter.ParameterType);
                //    parametersForInvocation.Add(defaultValue);
                //}
            }
            return methodInfo.Invoke(instance, parametersForInvocation.ToArray());
        }

        /// <summary>
        /// Gets the type associated with the specified name.
        /// </summary>
        /// <param name="typeName">Full name of the type.</param>
        /// <param name="type">The type.</param>
        /// <param name="customAssemblies">Additional loaded assemblies (optional).</param>
        /// <returns>Returns <c>true</c> if the type was found; otherwise <c>false</c>.</returns>
        public static bool TryGetTypeByName(string typeName, out Type type, params Assembly[] customAssemblies)
        {
            // remove full qualified assembly type name
            if (typeName.Contains("Version=") && !typeName.Contains("`"))
                typeName = typeName.Substring(0, typeName.IndexOf(','));

            type = Type.GetType(typeName) ?? GetTypeFromAssemblies(typeName, customAssemblies);

            // try get generic types
            if (type == null && typeName.Contains("'"))
            {
                var match = Regex.Match(typeName, "(?<MainType>.+`(?<ParamCount>[0-9]+))\\[(?<Types>.*)\\]");

                if (match.Success)
                {
                    var genericParameterCount = int.Parse(match.Groups["ParamCount"].Value);
                    var genericDef = match.Groups["Types"].Value;
                    var typeArgs = new List<string>(genericParameterCount);
                    typeArgs.AddRange(from Match typeArgMatch in Regex.Matches(genericDef, "\\[(?<Type>.*?)\\],?") where typeArgMatch.Success select typeArgMatch.Groups["Type"].Value.Trim());

                    var genericArgumentTypes = new Type[typeArgs.Count];
                    for (var genTypeIndex = 0; genTypeIndex < typeArgs.Count; genTypeIndex++)
                    {
                        Type genericType;
                        if (TryGetTypeByName(typeArgs[genTypeIndex], out genericType, customAssemblies))
                            genericArgumentTypes[genTypeIndex] = genericType;
                        else
                            // cant find generic type
                            return false;
                    }

                    var genericTypeString = match.Groups["MainType"].Value;
                    Type genericMainType;
                    if (TryGetTypeByName(genericTypeString, out genericMainType))
                        // make generic type
                        type = genericMainType.MakeGenericType(genericArgumentTypes);
                }
            }

            return type != null;
        }

        private static Type GetTypeFromAssemblies(string typeName, params Assembly[] customAssemblies)
        {
            Type type;

            if (customAssemblies != null && customAssemblies.Length > 0)
                foreach (var assembly in customAssemblies)
                {
                    type = assembly.GetType(typeName);

                    if (type != null)
                        return type;
                }

            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in loadedAssemblies)
            {
                type = assembly.GetType(typeName);

                if (type != null)
                    return type;
            }

            return null;
        }
    }
    class State
    {
        public int Category;
        public int Combo;
        public int Node;
        public string Token;
        public bool IsMaximized;
        public int SplitMajor;
        public int SplitMinor;
    }

    class ConstructorInfo
    {
        public Type[] ConstorParamType;
        public object[] ConstorParams;
    }
}
