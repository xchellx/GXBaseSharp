using System;
using UtilSharp.DataAnnotations;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace BNRSharp.Serialization.YAML
{
    public class YAMLValidatingNodeDeserializer(INodeDeserializer nodeDeserializer, bool? defaultValue = null,
        Predicate<object?>? defaultValuePredicate = null) : INodeDeserializer
    {
        public bool Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value, ObjectDeserializer rootDeserializer)
        {
            if (nodeDeserializer.Deserialize(parser, expectedType, nestedObjectDeserializer, out value, rootDeserializer) && value != null)
            {
                if (value is IOptionsValidatorProvider validatorProvider)
                {
                    validatorProvider.Validator.Validate(null, value);
                    return true;
                }
                else
                    return defaultValue ?? defaultValuePredicate?.Invoke(value) ?? false;
            }
            else
                return false;
        }
    }
}
