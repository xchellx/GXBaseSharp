using BNRSharp.Serialization;
using BNRSharp.Serialization.YAML;
using GXTextureSharp;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Tga;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors.Quantization;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using UtilSharp.DataAnnotations;
using UtilSharp.Util;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization.NodeDeserializers;
using static BNRSharp.Serialization.BNRConstants;

namespace BNRSharp
{
    public static partial class BNRHandler
    {
        public static bool IsMagicBNR1(uint magic)
            => IsMagicBNR1(Encoding.ASCII.GetString([.. BitConverter.GetBytes(magic)]));

        public static bool IsMagicBNR1(string magic) => MAGIC_BNR1.Equals(magic);

        public static bool IsMagicBNR2(uint magic)
            => IsMagicBNR2(Encoding.ASCII.GetString([.. BitConverter.GetBytes(magic)]));

        public static bool IsMagicBNR2(string magic) => MAGIC_BNR2.Equals(magic);

        public static void DecodeImage(IBNR bnr, bool useTga, bool compressed, Stream outputStream)
        {
            if (bnr.Image.Length == 0)
                throw new ArgumentException("Invalid image data length", nameof(bnr));

            GXTexture.DecodeImage(IMAGE_FORMAT, IMAGE_WIDTH, IMAGE_HEIGHT, bnr.Image, out uint[] decImg,
                new GXTexture.GXDecodeOptions());

            using Image<Bgra32> img = Image.LoadPixelData<Bgra32>(MemoryMarshal.AsBytes(decImg.AsSpan()),
                IMAGE_WIDTH, IMAGE_HEIGHT);
            if (useTga)
            {
                img.SaveAsTga(outputStream, new TgaEncoder()
                {
                    BitsPerPixel = TgaBitsPerPixel.Pixel32,
                    Compression = compressed ? TgaCompression.RunLength : TgaCompression.None
                });
            }
            else
            {
                using Image<Rgba32> imgCnv = img.CloneAs<Rgba32>();
                imgCnv.SaveAsPng(outputStream, new PngEncoder()
                {
                    PixelSamplingStrategy = new ExtensivePixelSamplingStrategy(),
                    BitDepth = PngBitDepth.Bit8,
                    ColorType = PngColorType.RgbWithAlpha,
                    FilterMethod = compressed ? PngFilterMethod.Paeth : PngFilterMethod.None,
                    CompressionLevel = compressed ? PngCompressionLevel.Level9 : PngCompressionLevel.NoCompression,
                    TextCompressionThreshold = 1024,
                    Gamma = null,
                    Threshold = byte.MaxValue,
                    InterlaceMethod = PngInterlaceMode.None,
                    ChunkFilter = PngChunkFilter.None,
                    TransparentColorMode = PngTransparentColorMode.Preserve
                });
            }
        }

        public static void DecodeInfo(bool isBnr2, IBNR bnr, Encoding encoding, Stream outputStream)
        {
            if (!IOUtil.Windows1252.Equals(encoding) && !IOUtil.ShiftJIS.Equals(encoding))
                throw new ArgumentException("Invalid encoding", nameof(bnr));

            using StreamWriter writer = new(outputStream, encoding, leaveOpen: true);
            if (!isBnr2 && bnr is BNR1 bnr1 && (encoding.Equals(IOUtil.ShiftJIS)
                || encoding.Equals(IOUtil.Windows1252)))
            {
                YAMLBNR1 yBnr1 = new()
                {
                    IsBNR2_V = false,
                    EnglishOrJapaneseInfo = new YAMLBNRInfo()
                    {
                        ShortTitle = bnr1.EnglishOrJapaneseInfo.ShortTitle,
                        ShortMaker = bnr1.EnglishOrJapaneseInfo.ShortMaker,
                        LongTitle = bnr1.EnglishOrJapaneseInfo.LongTitle,
                        LongMaker = bnr1.EnglishOrJapaneseInfo.LongMaker,
                        Comment = bnr1.EnglishOrJapaneseInfo.Comment
                    },
                };

                ((IOptionsValidatorProvider) yBnr1).Validator.Validate(null, yBnr1);

                writer.Write(new StaticSerializerBuilder(new YAMLStaticContext())
                    .WithQuotingNecessaryStrings()
                    .WithNamingConvention(UnderscoredNamingConvention.Instance)
                    .ConfigureDefaultValuesHandling(DefaultValuesHandling.Preserve)
                    .WithDefaultScalarStyle(ScalarStyle.Literal)
                    .EnsureRoundtrip()
                    .Build()
                    .Serialize(yBnr1));
            }
            else if (isBnr2 && bnr is BNR2 bnr2 && encoding.Equals(IOUtil.Windows1252))
            {
                YAMLBNR2 yBnr2 = new()
                {
                    IsBNR2_V = true,
                    EnglishInfo = new YAMLBNRInfo()
                    {
                        ShortTitle = bnr2.EnglishInfo.ShortTitle,
                        ShortMaker = bnr2.EnglishInfo.ShortMaker,
                        LongTitle = bnr2.EnglishInfo.LongTitle,
                        LongMaker = bnr2.EnglishInfo.LongMaker,
                        Comment = bnr2.EnglishInfo.Comment
                    },
                    GermanInfo = new YAMLBNRInfo()
                    {
                        ShortTitle = bnr2.GermanInfo.ShortTitle,
                        ShortMaker = bnr2.GermanInfo.ShortMaker,
                        LongTitle = bnr2.GermanInfo.LongTitle,
                        LongMaker = bnr2.GermanInfo.LongMaker,
                        Comment = bnr2.GermanInfo.Comment
                    },
                    FrenchInfo = new YAMLBNRInfo()
                    {
                        ShortTitle = bnr2.FrenchInfo.ShortTitle,
                        ShortMaker = bnr2.FrenchInfo.ShortMaker,
                        LongTitle = bnr2.FrenchInfo.LongTitle,
                        LongMaker = bnr2.FrenchInfo.LongMaker,
                        Comment = bnr2.FrenchInfo.Comment
                    },
                    SpanishInfo = new YAMLBNRInfo()
                    {
                        ShortTitle = bnr2.SpanishInfo.ShortTitle,
                        ShortMaker = bnr2.SpanishInfo.ShortMaker,
                        LongTitle = bnr2.SpanishInfo.LongTitle,
                        LongMaker = bnr2.SpanishInfo.LongMaker,
                        Comment = bnr2.SpanishInfo.Comment
                    },
                    ItalianInfo = new YAMLBNRInfo()
                    {
                        ShortTitle = bnr2.ItalianInfo.ShortTitle,
                        ShortMaker = bnr2.ItalianInfo.ShortMaker,
                        LongTitle = bnr2.ItalianInfo.LongTitle,
                        LongMaker = bnr2.ItalianInfo.LongMaker,
                        Comment = bnr2.ItalianInfo.Comment
                    },
                    DutchInfo = new YAMLBNRInfo()
                    {
                        ShortTitle = bnr2.DutchInfo.ShortTitle,
                        ShortMaker = bnr2.DutchInfo.ShortMaker,
                        LongTitle = bnr2.DutchInfo.LongTitle,
                        LongMaker = bnr2.DutchInfo.LongMaker,
                        Comment = bnr2.DutchInfo.Comment
                    }
                };

                ((IOptionsValidatorProvider) yBnr2).Validator.Validate(null, yBnr2);

                writer.Write(new StaticSerializerBuilder(new YAMLStaticContext())
                    .WithQuotingNecessaryStrings()
                    .WithNamingConvention(UnderscoredNamingConvention.Instance)
                    .ConfigureDefaultValuesHandling(DefaultValuesHandling.Preserve)
                    .WithDefaultScalarStyle(ScalarStyle.Literal)
                    .EnsureRoundtrip()
                    .Build()
                    .Serialize(yBnr2));
            }
            else
                throw new ArgumentException("Invalid BNR and/or encoding", nameof(bnr));
        }

        public static bool IsInfoBNR2(BNR bnr, Stream stream)
        {
            using StreamReader reader = new(stream, IOUtil.Windows1252, leaveOpen: true);
            object? yBnr = new StaticDeserializerBuilder(new YAMLStaticContext())
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .WithDuplicateKeyChecking()
                .WithNodeDeserializer(
                    inner => new YAMLValidatingNodeDeserializer(inner),
                    s => s.InsteadOf<ObjectNodeDeserializer>()
                )
                .Build()
                .Deserialize<YAMLBNR>(reader.ReadLine() ?? string.Empty);

            if (yBnr is IYAMLBNR yBnrSpec)
            {
                if (yBnrSpec is YAMLBNR)
                {
                    bool isBnr2 = yBnrSpec.IsBNR2;
                    bnr.Magic = isBnr2 ? MAGIC_BNR2 : MAGIC_BNR1;
                    return isBnr2;
                }
                else
                    throw new InvalidOperationException("YAML did not return a valid structure (it may be malformed)");
            }
            else
                throw new InvalidOperationException("YAML did not return a valid structure (it may be malformed)");
        }

        public static void EncodeImage(IBNR bnr, bool useTga, Stream stream)
        {
            uint[] imgData;
            if (useTga) {
                using Image<Bgra32> img = TgaDecoder.Instance.Decode<Bgra32>(new DecoderOptions(), stream);
                imgData = new uint[IMAGE_WIDTH * IMAGE_HEIGHT];
                img.CopyPixelDataTo(MemoryMarshal.AsBytes(imgData.AsSpan()));
            }
            else
            {
                using Image<Rgba32> img = PngDecoder.Instance.Decode<Rgba32>(new PngDecoderOptions()
                {
                    GeneralOptions = new DecoderOptions(),
                    PngCrcChunkHandling = PngCrcChunkHandling.IgnoreNone,
                    MaxUncompressedAncillaryChunkSizeBytes = 8 * 1024 * 1024
                }, stream);
                using Image<Bgra32> imgCnv = img.CloneAs<Bgra32>();
                imgData = new uint[IMAGE_WIDTH * IMAGE_HEIGHT];
                imgCnv.CopyPixelDataTo(MemoryMarshal.AsBytes(imgData.AsSpan()));
            }

            GXTexture.EncodeImage(IMAGE_FORMAT, IMAGE_WIDTH, IMAGE_HEIGHT, imgData, out byte[] tmpImage,
                new GXTexture.GXEncodeOptions());
            bnr.Image = tmpImage;
        }

        public static void EncodeInfo(bool isBnr2, IBNR bnr, Encoding encoding, Stream stream)
        {
            if (!IOUtil.Windows1252.Equals(encoding) && !IOUtil.ShiftJIS.Equals(encoding))
                throw new ArgumentException("Invalid encoding", nameof(bnr));

            using StreamReader reader = new(stream, encoding, leaveOpen: true);
            IDeserializer yBnrSer = new StaticDeserializerBuilder(new YAMLStaticContext())
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .WithDuplicateKeyChecking()
                .WithNodeDeserializer(
                    inner => new YAMLValidatingNodeDeserializer(inner),
                    s => s.InsteadOf<ObjectNodeDeserializer>()
                )
                .Build();

            object? yBnr = isBnr2 ? yBnrSer.Deserialize<YAMLBNR2>(reader) : yBnrSer.Deserialize<YAMLBNR1>(reader);
            if (yBnr is IYAMLBNR yBnrSpec1 && !yBnrSpec1.IsBNR2 && yBnrSpec1 is YAMLBNR1 yBnr1 && bnr is BNR1 bnr1)
            {
                bnr1.EnglishOrJapaneseInfo.ShortTitle = yBnr1.EnglishOrJapaneseInfo.ShortTitle;
                bnr1.EnglishOrJapaneseInfo.ShortMaker = yBnr1.EnglishOrJapaneseInfo.ShortMaker;
                bnr1.EnglishOrJapaneseInfo.LongTitle = yBnr1.EnglishOrJapaneseInfo.LongTitle;
                bnr1.EnglishOrJapaneseInfo.LongMaker = yBnr1.EnglishOrJapaneseInfo.LongMaker;
                bnr1.EnglishOrJapaneseInfo.Comment = CRLFToLF().Replace(yBnr1.EnglishOrJapaneseInfo.Comment, "\n");
            }
            else if (yBnr is IYAMLBNR yBnrSpec2 && yBnrSpec2.IsBNR2 && yBnrSpec2 is YAMLBNR2 yBnr2 && bnr is BNR2 bnr2)
            {
                bnr2.EnglishInfo.ShortTitle = yBnr2.EnglishInfo.ShortTitle;
                bnr2.EnglishInfo.ShortMaker = yBnr2.EnglishInfo.ShortMaker;
                bnr2.EnglishInfo.LongTitle = yBnr2.EnglishInfo.LongTitle;
                bnr2.EnglishInfo.LongMaker = yBnr2.EnglishInfo.LongMaker;
                bnr2.EnglishInfo.Comment = CRLFToLF().Replace(yBnr2.EnglishInfo.Comment, "\n");

                bnr2.GermanInfo.ShortTitle = yBnr2.GermanInfo.ShortTitle;
                bnr2.GermanInfo.ShortMaker = yBnr2.GermanInfo.ShortMaker;
                bnr2.GermanInfo.LongTitle = yBnr2.GermanInfo.LongTitle;
                bnr2.GermanInfo.LongMaker = yBnr2.GermanInfo.LongMaker;
                bnr2.GermanInfo.Comment = CRLFToLF().Replace(yBnr2.GermanInfo.Comment, "\n");

                bnr2.FrenchInfo.ShortTitle = yBnr2.FrenchInfo.ShortTitle;
                bnr2.FrenchInfo.ShortMaker = yBnr2.FrenchInfo.ShortMaker;
                bnr2.FrenchInfo.LongTitle = yBnr2.FrenchInfo.LongTitle;
                bnr2.FrenchInfo.LongMaker = yBnr2.FrenchInfo.LongMaker;
                bnr2.FrenchInfo.Comment = CRLFToLF().Replace(yBnr2.FrenchInfo.Comment, "\n");

                bnr2.SpanishInfo.ShortTitle = yBnr2.SpanishInfo.ShortTitle;
                bnr2.SpanishInfo.ShortMaker = yBnr2.SpanishInfo.ShortMaker;
                bnr2.SpanishInfo.LongTitle = yBnr2.SpanishInfo.LongTitle;
                bnr2.SpanishInfo.LongMaker = yBnr2.SpanishInfo.LongMaker;
                bnr2.SpanishInfo.Comment = CRLFToLF().Replace(yBnr2.SpanishInfo.Comment, "\n");

                bnr2.ItalianInfo.ShortTitle = yBnr2.ItalianInfo.ShortTitle;
                bnr2.ItalianInfo.ShortMaker = yBnr2.ItalianInfo.ShortMaker;
                bnr2.ItalianInfo.LongTitle = yBnr2.ItalianInfo.LongTitle;
                bnr2.ItalianInfo.LongMaker = yBnr2.ItalianInfo.LongMaker;
                bnr2.ItalianInfo.Comment = CRLFToLF().Replace(yBnr2.ItalianInfo.Comment, "\n");

                bnr2.DutchInfo.ShortTitle = yBnr2.DutchInfo.ShortTitle;
                bnr2.DutchInfo.ShortMaker = yBnr2.DutchInfo.ShortMaker;
                bnr2.DutchInfo.LongTitle = yBnr2.DutchInfo.LongTitle;
                bnr2.DutchInfo.LongMaker = yBnr2.DutchInfo.LongMaker;
                bnr2.DutchInfo.Comment = CRLFToLF().Replace(yBnr2.DutchInfo.Comment, "\n");
            }
            else
                throw new InvalidOperationException("YAML did not return a valid structure (it may be malformed)");
        }

        [GeneratedRegex("(?:\\r\\n|\\r)", RegexOptions.Multiline | RegexOptions.CultureInvariant)]
        private static partial Regex CRLFToLF();
    }
}
