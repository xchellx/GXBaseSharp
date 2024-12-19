using System;
using System.Diagnostics.CodeAnalysis;

namespace BNRSharp.Serialization.YAML
{
    public abstract class AYAMLBNR : IYAMLBNR
    {
        [NotNull]
        public abstract bool? IsBNR2_V { get; set; }

        public bool IsBNR2 { get => IsBNR2_V.Value; set => IsBNR2_V = value; }
    }
}
