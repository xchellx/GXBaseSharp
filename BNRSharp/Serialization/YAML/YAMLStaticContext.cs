using BNRSharp.Serialization.YAML;
using YamlDotNet.Serialization;

namespace BNRSharp
{
    [YamlStaticContext]
    [YamlSerializable(typeof(YAMLBNR))]
    [YamlSerializable(typeof(YAMLBNR1))]
    [YamlSerializable(typeof(YAMLBNR2))]
    [YamlSerializable(typeof(YAMLBNRInfo))]
    public partial class YAMLStaticContext : StaticContext
    {
    }
}
