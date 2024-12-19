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
    public sealed partial class YAMLBNR1 : AYAMLBNR, IOptionsValidatorProvider<YAMLBNR1>
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
        [AllowedValues(false, ErrorMessage = "A BNR1 info must specify that it represents a BNR1 file")]
        public override bool? IsBNR2_V { get; set; } = false;

        [YamlMember(
            Alias = "english_or_japanese",
            ApplyNamingConventions = true,
            DefaultValuesHandling = DefaultValuesHandling.Preserve,
            ScalarStyle = ScalarStyle.Literal
        )]
        [DefaultValue(null)]
        [NotNull]
        [Required]
        [ValidateObjectMembers]
        public YAMLBNRInfo? EnglishOrJapaneseInfo { get; set; } = null;

        IOptionsValidator<YAMLBNR1> IOptionsValidatorProvider<YAMLBNR1>.Validator => Validator.Instance;

        IOptionsValidator IOptionsValidatorProvider.Validator => Validator.Instance;

        [OptionsValidator]
        public sealed partial class Validator : IOptionsValidator<YAMLBNR1>
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
