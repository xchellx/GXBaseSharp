using System;
using System.Diagnostics.CodeAnalysis;
using YamlDotNet.Serialization;

namespace BNRSharp.Serialization.YAML
{
    public abstract class AYAMLBNR : IYAMLBNR
    {
        [NotNull]
        [YamlIgnore]
        public abstract bool? IsBNR2_V { get; set; }

        [YamlIgnore]
        public bool IsBNR2 { get => IsBNR2_V.Value; set => IsBNR2_V = value; }
    }
}
