using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using UtilSharp.DataAnnotations;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace BNRSharp.Serialization.YAML
{
    [YamlSerializable]
    public sealed partial class YAMLBNR : AYAMLBNR, IOptionsValidatorProvider<YAMLBNR>
    {
        [YamlMember(
            Alias = "is_bnr2",
            ApplyNamingConventions = true,
            DefaultValuesHandling = DefaultValuesHandling.Preserve,
            ScalarStyle = ScalarStyle.ForcePlain
        )]
        [DefaultValue(false)]
        [NotNull]
        [Required]
        [AllowedValues(false, true, ErrorMessage = "Unexpected value for BNR's representation")]
        public override bool? IsBNR2_V { get; set; } = false;

        IOptionsValidator<YAMLBNR> IOptionsValidatorProvider<YAMLBNR>.Validator => Validator.Instance;

        IOptionsValidator IOptionsValidatorProvider.Validator => Validator.Instance;

        [OptionsValidator]
        public sealed partial class Validator : IOptionsValidator<YAMLBNR>
        {
            private Validator() { }

            public static Validator Instance => EmptyInstance.Value;

            private static class EmptyInstance
            {
                internal static readonly Validator Value = new();
            }
        }
    }
}
