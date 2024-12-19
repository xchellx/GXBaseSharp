using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using static GXTextureSharp.GXTexture;

namespace GXTextureSharp
{
    public static partial class GXTexture
    {
        public static int GetImageDataSize(GXFormat format, ushort width, ushort height)
        {
            if (width == 0)
                throw new ArgumentException("Invalid width", nameof(width));
            else if (height == 0)
                throw new ArgumentException("Invalid height", nameof(height));

            byte bpp = format switch
            {
                GXFormat.I4 => gxtexture.GX_I4_BPP,
                GXFormat.I8 => gxtexture.GX_I8_BPP,
                GXFormat.IA4 => gxtexture.GX_IA4_BPP,
                GXFormat.IA8 => gxtexture.GX_IA8_BPP,
                GXFormat.CI4 => gxtexture.GX_CI4_BPP,
                GXFormat.CI8 => gxtexture.GX_CI8_BPP,
                GXFormat.CI14X2 => gxtexture.GX_CI14X2_BPP,
                GXFormat.R5G6B5 => gxtexture.GX_R5G6B5_BPP,
                GXFormat.RGB5A3 => gxtexture.GX_RGB5A3_BPP,
                GXFormat.RGBA8 => gxtexture.GX_RGBA8_BPP,
                GXFormat.CMP => gxtexture.GX_CMP_BPP,
                _ => throw new ArgumentException("Invalid texture format", nameof(format))
            };

            return (int) Math.Min(gxtexture.GX_CalcMipSz_64(width, height, bpp), int.MaxValue);
        }

        public static int GetPaletteDataMaxSize(GXFormat format, ushort width, ushort height)
        {
            if (width == 0)
                throw new ArgumentException("Invalid width", nameof(width));
            else if (height == 0)
                throw new ArgumentException("Invalid height", nameof(height));

            byte bpp = format switch
            {
                GXFormat.CI4 => gxtexture.GX_CI4_BPP,
                GXFormat.CI8 => gxtexture.GX_CI8_BPP,
                GXFormat.CI14X2 => gxtexture.GX_CI14X2_BPP,
                _ => throw new ArgumentException("Invalid texture format", nameof(format))
            };

            return (int) Math.Min(gxtexture.GX_GetMaxPalSz_64(bpp), int.MaxValue);
        }

        public static int DecodeImage(GXFormat format, ushort width, ushort height, byte[] data, out uint[] outData,
            GXDecodeOptions opts)
        {
            if (width == 0)
                throw new ArgumentException("Invalid width", nameof(width));
            else if (height == 0)
                throw new ArgumentException("Invalid height", nameof(height));
            else if (data.Length == 0 || data.Length > GetImageDataSize(format, width, height))
                throw new ArgumentException("Invalid data length", nameof(height));

            ulong dataSz = (ulong) Math.Max(data.Length, 0);
            ulong outDataSz = (ulong) Math.Clamp(width * height, 0, int.MaxValue);
            outData = Enumerable.Repeat((uint) 0, (int) outDataSz).ToArray();

            ulong read = format switch
            {
                GXFormat.I4 => gxtexture.GX_DecodeI4_64(width, height, dataSz, data, outDataSz, outData, opts.raw),
                GXFormat.I8 => gxtexture.GX_DecodeI8_64(width, height, dataSz, data, outDataSz, outData, opts.raw),
                GXFormat.IA4 => gxtexture.GX_DecodeIA4_64(width, height, dataSz, data, outDataSz, outData, opts.raw),
                GXFormat.IA8 => gxtexture.GX_DecodeIA8_64(width, height, dataSz, data, outDataSz, outData, opts.raw),
                GXFormat.R5G6B5 => gxtexture.GX_DecodeR5G6B5_64(width, height, dataSz, data, outDataSz, outData,
                    opts.raw),
                GXFormat.RGB5A3 => gxtexture.GX_DecodeRGB5A3_64(width, height, dataSz, data, outDataSz, outData,
                    opts.raw),
                GXFormat.RGBA8 => gxtexture.GX_DecodeRGBA8_64(width, height, dataSz, data, outDataSz, outData,
                    opts.raw),
                GXFormat.CMP => gxtexture.GX_DecodeCMP_64(width, height, dataSz, data, outDataSz, outData, opts.raw),
                _ => throw new ArgumentException("Invalid texture format", nameof(format))
            };

            if (read == 0)
                throw new InvalidOperationException("No data was decoded");

            return (int) Math.Min(read, int.MaxValue);
        }

        public static int DecodeIndexedImage(GXFormat format, GXPaletteFormat palFormat, ushort width, ushort height,
            byte[] data, uint[] pal, out uint[] outData, GXDecodeOptions opts)
        {
            if (width == 0)
                throw new ArgumentException("Invalid width", nameof(width));
            else if (height == 0)
                throw new ArgumentException("Invalid height", nameof(height));
            else if (data.Length == 0 || data.Length > GetImageDataSize(format, width, height))
                throw new ArgumentException("Invalid data length", nameof(data));
            else if (pal.Length == 0 || pal.Length > GetPaletteDataMaxSize(format, width, height))
                throw new ArgumentException("Invalid palette length", nameof(pal));

            ulong dataSz = (ulong) data.Length;
            ulong outDataSz = (ulong) Math.Clamp(width * height, 0, int.MaxValue);
            outData = Enumerable.Repeat((uint) 0, (int) outDataSz).ToArray();
            ulong palSz = (ulong) pal.Length;

            ulong read = format switch
            {
                GXFormat.CI4 => gxtexture.GX_DecodeCI4_64(width, height, dataSz, data, palSz, pal, outDataSz, outData,
                    opts.raw),
                GXFormat.CI8 => gxtexture.GX_DecodeCI8_64(width, height, dataSz, data, palSz, pal, outDataSz, outData,
                    opts.raw),
                GXFormat.CI14X2 => gxtexture.GX_DecodeCI14X2_64(width, height, dataSz, data, palSz, pal, outDataSz,
                    outData, opts.raw),
                _ => throw new ArgumentException("Invalid texture format", nameof(format))
            };

            if (read == 0)
                throw new InvalidOperationException("No data was decoded");

            return (int) Math.Min(read, int.MaxValue);
        }

        public static void DecodePalette(GXFormat format, GXPaletteFormat palFormat, ushort width, ushort height,
            ushort[] pal, out uint[] outPal, GXDecodeOptions opts)
        {
            if (width == 0)
                throw new ArgumentException("Invalid width", nameof(width));
            else if (height == 0)
                throw new ArgumentException("Invalid height", nameof(height));
            else if (pal.Length == 0 || pal.Length > GetPaletteDataMaxSize(format, width, height))
                throw new ArgumentException("Invalid palette length", nameof(pal));

            ulong palSz = (ulong) pal.Length;
            outPal = Enumerable.Repeat((uint) 0, (int) palSz).ToArray();

            bool palFail = palFormat switch
            {
                GXPaletteFormat.I8 => gxtexture.GX_DecodePaletteI8_64(palSz, pal, outPal, opts.raw),
                GXPaletteFormat.R5G6B5 => gxtexture.GX_DecodePaletteR5G6B5_64(palSz, pal, outPal, opts.raw),
                GXPaletteFormat.RGB5A3 => gxtexture.GX_DecodePaletteRGB5A3_64(palSz, pal, outPal, opts.raw),
                _ => throw new ArgumentException("Invalid palette format", nameof(palFormat))
            };

            if (palFail)
                throw new InvalidOperationException("Failed to decode palette");
        }

        public static int EncodeImage(GXFormat format, ushort width, ushort height, uint[] data, out byte[] outData,
            GXEncodeOptions opts)
        {
            if (width == 0)
                throw new ArgumentException("Invalid width", nameof(width));
            else if (height == 0)
                throw new ArgumentException("Invalid height", nameof(height));
            else if (data.Length == 0 || data.Length > width * height)
                throw new ArgumentException("Invalid data length", nameof(height));

            ulong dataSz = (ulong) data.Length;
            ulong outDataSz = (ulong) GetImageDataSize(format, width, height);
            outData = Enumerable.Repeat((byte) 0, (int) outDataSz).ToArray();

            ulong write;
            unsafe
            {
                fixed (float* squishMetricNative = &ArrayMarshaller<float, float>.ManagedToUnmanagedIn
                    .GetPinnableReference(opts.squishMetric))
                {
                    opts.raw.squishMetric = squishMetricNative;
                    write = format switch
                    {
                        GXFormat.I4 => gxtexture.GX_EncodeI4_64(width, height, dataSz, data, outDataSz, outData,
                            opts.raw),
                        GXFormat.I8 => gxtexture.GX_EncodeI8_64(width, height, dataSz, data, outDataSz, outData,
                            opts.raw),
                        GXFormat.IA4 => gxtexture.GX_EncodeIA4_64(width, height, dataSz, data, outDataSz, outData,
                            opts.raw),
                        GXFormat.IA8 => gxtexture.GX_EncodeIA8_64(width, height, dataSz, data, outDataSz, outData,
                            opts.raw),
                        GXFormat.R5G6B5 => gxtexture.GX_EncodeR5G6B5_64(width, height, dataSz, data, outDataSz,
                            outData, opts.raw),
                        GXFormat.RGB5A3 => gxtexture.GX_EncodeRGB5A3_64(width, height, dataSz, data, outDataSz,
                            outData, opts.raw),
                        GXFormat.RGBA8 => gxtexture.GX_EncodeRGBA8_64(width, height, dataSz, data, outDataSz,
                            outData, opts.raw),
                        GXFormat.CMP => gxtexture.GX_EncodeCMP_64(width, height, dataSz, data, outDataSz, outData,
                            opts.raw),
                        _ => throw new ArgumentException("Invalid texture format", nameof(format))
                    };
                }
            }

            if (write == 0)
                throw new InvalidOperationException("No data was encoded");

            return (int) Math.Min(write, int.MaxValue);
        }

        public static int EncodeIndexedImage(GXFormat format, GXPaletteFormat palFormat, ushort width, ushort height,
            uint[] indexData, int palSize, out byte[] outIndexData, GXEncodeOptions opts)
        {
            if (width == 0)
                throw new ArgumentException("Invalid width", nameof(width));
            else if (height == 0)
                throw new ArgumentException("Invalid height", nameof(height));
            else if (indexData.Length == 0 || indexData.Length > width * height)
                throw new ArgumentException("Invalid index data length", nameof(indexData));
            else if (palSize == 0 || palSize > GetPaletteDataMaxSize(format, width, height))
                throw new ArgumentException("Invalid palette length", nameof(palSize));

            ulong indexDataSz = (ulong) indexData.Length;
            ulong outIndexDataSz = (ulong) GetImageDataSize(format, width, height);
            outIndexData = Enumerable.Repeat((byte) 0, (int) outIndexDataSz).ToArray();
            ulong palSz = (ulong) palSize;

            ulong write;
            unsafe
            {
                fixed (float* squishMetricNative = &ArrayMarshaller<float, float>.ManagedToUnmanagedIn
                    .GetPinnableReference(opts.squishMetric))
                {
                    opts.raw.squishMetric = squishMetricNative;
                    write = format switch
                    {
                        GXFormat.CI4 => gxtexture.GX_EncodeCI4_64(width, height, indexDataSz, indexData, palSz,
                            outIndexDataSz, outIndexData,  opts.raw),
                        GXFormat.CI8 => gxtexture.GX_EncodeCI8_64(width, height, indexDataSz, indexData, palSz,
                            outIndexDataSz, outIndexData, opts.raw),
                        GXFormat.CI14X2 => gxtexture.GX_EncodeCI14X2_64(width, height, indexDataSz, indexData, palSz,
                            outIndexDataSz, outIndexData, opts.raw),
                        _ => throw new ArgumentException("Invalid texture format", nameof(format))
                    };
                }
            }

            if (write == 0)
                throw new InvalidOperationException("No data was encoded");

            return (int) Math.Min(write, int.MaxValue);
        }

        public static void EncodePalette(GXFormat format, GXPaletteFormat palFormat, ushort width, ushort height,
            uint[] pal, out ushort[] outPal, GXEncodeOptions opts)
        {
            if (width == 0)
                throw new ArgumentException("Invalid width", nameof(width));
            else if (height == 0)
                throw new ArgumentException("Invalid height", nameof(height));
            else if (pal.Length == 0 || pal.Length > GetPaletteDataMaxSize(format, width, height))
                throw new ArgumentException("Invalid palette length", nameof(pal));

            ulong palSz = (ulong) pal.Length;
            outPal = Enumerable.Repeat((ushort) 0, (int) palSz).ToArray();

            bool palFail;
            unsafe
            {
                fixed (float* squishMetricNative = &ArrayMarshaller<float, float>.ManagedToUnmanagedIn
                    .GetPinnableReference(opts.squishMetric))
                {
                    opts.raw.squishMetric = squishMetricNative;
                    palFail = palFormat switch
                    {
                        GXPaletteFormat.I8 => gxtexture.GX_EncodePaletteI8_64(palSz, pal, outPal, opts.raw),
                        GXPaletteFormat.R5G6B5 => gxtexture.GX_EncodePaletteR5G6B5_64(palSz, pal, outPal, opts.raw),
                        GXPaletteFormat.RGB5A3 => gxtexture.GX_EncodePaletteRGB5A3_64(palSz, pal, outPal, opts.raw),
                        _ => throw new ArgumentException("Invalid palette format", nameof(palFormat))
                    };
                }
            }

            if (palFail)
                throw new InvalidOperationException("Failed to encode palette");
        }

        public static void BuildPalette(GXFormat format, ushort width, ushort height, uint[] data, int palSize,
            out int outPalSize, out uint[] outPal, out uint[] outIndexData, GXEncodeOptions opts)
        {
            if (width == 0)
                throw new ArgumentException("Invalid width", nameof(width));
            else if (height == 0)
                throw new ArgumentException("Invalid height", nameof(height));
            else if (data.Length == 0 || data.Length > width * height)
                throw new ArgumentException("Invalid data length", nameof(height));
            else if (palSize == 0 || palSize > GetPaletteDataMaxSize(format, width, height))
                throw new ArgumentException("Invalid palette length", nameof(palSize));

            ulong dataSz = (ulong) data.Length;
            ulong outIndexDataSz = (ulong) (width * height);
            outIndexData = Enumerable.Repeat((uint) 0, (int) outIndexDataSz).ToArray();
            ulong palSz = (ulong) palSize;
            outPal = Enumerable.Repeat((uint) 0, GetPaletteDataMaxSize(format, width, height)).ToArray();
            ulong outPalSz = 0;
            outPalSize = 0;

            bool palFail;
            unsafe
            {
                fixed (float* squishMetricNative = &ArrayMarshaller<float, float>.ManagedToUnmanagedIn
                    .GetPinnableReference(opts.squishMetric))
                {
                    opts.raw.squishMetric = squishMetricNative;
                    palFail = gxtexture.GX_BuildPalette_64(width, height, dataSz, data, palSz, outPal, outIndexDataSz,
                        outIndexData, out outPalSz, opts.raw);
                    outPalSize = (int) Math.Min(outPalSz, int.MaxValue);
                }
            }

            if (palFail)
                throw new InvalidOperationException("Failed to encode palette");
        }
    }
}
