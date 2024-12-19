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
    public sealed partial class YAMLBNR2 : AYAMLBNR, IOptionsValidatorProvider<YAMLBNR2>
    {
        [YamlMember(
            Alias = "is_bnr2",
            ApplyNamingConventions = true,
            DefaultValuesHandling = DefaultValuesHandling.Preserve,
            ScalarStyle = ScalarStyle.ForcePlain
        )]
        [DefaultValue(true)]
        [NotNull]
        [Required]
        [AllowedValues(true, ErrorMessage = "A BNR2 info must specify that it represents a BNR2 file")]
        public override bool? IsBNR2_V { get; set; } = true;

        [YamlMember(
            Alias = "english",
            ApplyNamingConventions = true,
            DefaultValuesHandling = DefaultValuesHandling.Preserve,
            ScalarStyle = ScalarStyle.Literal
        )]
        [DefaultValue(null)]
        [NotNull]
        [Required]
        [ValidateObjectMembers]
        public YAMLBNRInfo? EnglishInfo { get; set; } = null;

        [YamlMember(
            Alias = "german",
            ApplyNamingConventions = true,
            DefaultValuesHandling = DefaultValuesHandling.Preserve,
            ScalarStyle = ScalarStyle.Literal
        )]
        [DefaultValue(null)]
        [NotNull]
        [Required]
        [ValidateObjectMembers]
        public YAMLBNRInfo? GermanInfo { get; set; } = null;

        [YamlMember(
            Alias = "french",
            ApplyNamingConventions = true,
            DefaultValuesHandling = DefaultValuesHandling.Preserve,
            ScalarStyle = ScalarStyle.Literal
        )]
        [DefaultValue(null)]
        [NotNull]
        [Required]
        [ValidateObjectMembers]
        public YAMLBNRInfo? FrenchInfo { get; set; } = null;

        [YamlMember(
            Alias = "spanish",
            ApplyNamingConventions = true,
            DefaultValuesHandling = DefaultValuesHandling.Preserve,
            ScalarStyle = ScalarStyle.Literal
        )]
        [DefaultValue(null)]
        [NotNull]
        [Required]
        [ValidateObjectMembers]
        public YAMLBNRInfo? SpanishInfo { get; set; } = null;

        [YamlMember(
            Alias = "italian",
            ApplyNamingConventions = true,
            DefaultValuesHandling = DefaultValuesHandling.Preserve,
            ScalarStyle = ScalarStyle.Literal
        )]
        [DefaultValue(null)]
        [NotNull]
        [Required]
        [ValidateObjectMembers]
        public YAMLBNRInfo? ItalianInfo { get; set; } = null;

        [YamlMember(
            Alias = "dutch",
            ApplyNamingConventions = true,
            DefaultValuesHandling = DefaultValuesHandling.Preserve,
            ScalarStyle = ScalarStyle.Literal
        )]
        [DefaultValue(null)]
        [NotNull]
        [Required]
        [ValidateObjectMembers]
        public YAMLBNRInfo? DutchInfo { get; set; } = null;

        IOptionsValidator<YAMLBNR2> IOptionsValidatorProvider<YAMLBNR2>.Validator => Validator.Instance;

        IOptionsValidator IOptionsValidatorProvider.Validator => Validator.Instance;

        [OptionsValidator]
        public sealed partial class Validator : IOptionsValidator<YAMLBNR2>
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
