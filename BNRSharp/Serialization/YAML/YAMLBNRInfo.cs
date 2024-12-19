using BNRSharp.DataAnnotations;
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
    public sealed partial class YAMLBNRInfo : IOptionsValidatorProvider<YAMLBNRInfo>
    {
        [YamlMember(
            Alias = "short_title",
            ApplyNamingConventions = true,
            DefaultValuesHandling = DefaultValuesHandling.Preserve,
            ScalarStyle = ScalarStyle.Literal
        )]
        [DefaultValue(null)]
        [NotNull]
        [Required]
        public string? ShortTitle { get; set; } = null;

        [YamlMember(
            Alias = "short_maker",
            ApplyNamingConventions = true,
            DefaultValuesHandling = DefaultValuesHandling.Preserve,
            ScalarStyle = ScalarStyle.Literal
        )]
        [DefaultValue(null)]
        [NotNull]
        [Required]
        public string? ShortMaker { get; set; } = null;

        [YamlMember(
            Alias = "long_title",
            ApplyNamingConventions = true,
            DefaultValuesHandling = DefaultValuesHandling.Preserve,
            ScalarStyle = ScalarStyle.Literal
        )]
        [DefaultValue(null)]
        [NotNull]
        [Required]
        public string? LongTitle { get; set; } = null;

        [YamlMember(
            Alias = "long_maker",
            ApplyNamingConventions = true,
            DefaultValuesHandling = DefaultValuesHandling.Preserve,
            ScalarStyle = ScalarStyle.Literal
        )]
        [DefaultValue(null)]
        [NotNull]
        [Required]
        public string? LongMaker { get; set; } = null;

        [YamlMember(
            Alias = "comment",
            ApplyNamingConventions = true,
            DefaultValuesHandling = DefaultValuesHandling.Preserve,
            ScalarStyle = ScalarStyle.Literal
        )]
        [DefaultValue(null)]
        [NotNull]
        [Required]
        [StringLines(2, MinimumLines = 1)]
        public string? Comment { get; set; } = null;

        IOptionsValidator<YAMLBNRInfo> IOptionsValidatorProvider<YAMLBNRInfo>.Validator => Validator.Instance;

        IOptionsValidator IOptionsValidatorProvider.Validator => Validator.Instance;

        [OptionsValidator]
        public sealed partial class Validator : IOptionsValidator<YAMLBNRInfo>
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
