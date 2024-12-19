using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace GXTextureSharp
{
    public static partial class GXTexture
    {
#pragma warning disable CS8981
#pragma warning disable IDE1006
#pragma warning disable SYSLIB1092
        internal static partial class gxtexture
        {
            private const string libName = "libgxtexture";

            internal const byte GX_I4_BW = 8;
            internal const byte GX_I4_BH = 8;
            internal const byte GX_I4_BPP = 4;

            internal const byte GX_I8_BW = 8;
            internal const byte GX_I8_BH = 4;
            internal const byte GX_I8_BPP = 8;

            internal const byte GX_IA4_BW = 8;
            internal const byte GX_IA4_BH = 4;
            internal const byte GX_IA4_BPP = 8;

            internal const byte GX_IA8_BW = 4;
            internal const byte GX_IA8_BH = 4;
            internal const byte GX_IA8_BPP = 16;

            internal const byte GX_CI4_BW = 8;
            internal const byte GX_CI4_BH = 8;
            internal const byte GX_CI4_BPP = 4;
            internal const byte GX_CI4_PMUL = 4;

            internal const byte GX_CI8_BW = 8;
            internal const byte GX_CI8_BH = 4;
            internal const byte GX_CI8_BPP = 8;
            internal const byte GX_CI8_PMUL = 8;

            internal const byte GX_CI14X2_BW = 4;
            internal const byte GX_CI14X2_BH = 4;
            internal const byte GX_CI14X2_BPP = 16;
            internal const byte GX_CI14X2_PMUL = 14;

            internal const byte GX_R5G6B5_BW = 4;
            internal const byte GX_R5G6B5_BH = 4;
            internal const byte GX_R5G6B5_BPP = 16;

            internal const byte GX_RGB5A3_BW = 4;
            internal const byte GX_RGB5A3_BH = 4;
            internal const byte GX_RGB5A3_BPP = 16;

            internal const byte GX_RGBA8_BW = 4;
            internal const byte GX_RGBA8_BH = 4;
            internal const byte GX_RGBA8_BPP = 32;

            internal const byte GX_CMP_BW = 8;
            internal const byte GX_CMP_BH = 8;
            internal const byte GX_CMP_BPP = 4;

            // Misc
            [LibraryImport(libName, EntryPoint = "GX_CalcMipSz")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_CalcMipSz_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U1)] byte bpp
            );

            [LibraryImport(libName, EntryPoint = "GX_GetMaxPalSz")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_GetMaxPalSz_64(
                [MarshalAs(UnmanagedType.U1)] byte bpp
            );



            // Decode
            [LibraryImport(libName, EntryPoint = "GX_DecodeI4")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_DecodeI4_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] _in,
                [MarshalAs(UnmanagedType.U8)] ulong outSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] _out,
                in GXDecodeOptions_64 opt
            );

            [LibraryImport(libName, EntryPoint = "GX_DecodeI8")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_DecodeI8_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] _in,
                [MarshalAs(UnmanagedType.U8)] ulong outSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] _out,
                in GXDecodeOptions_64 opt
            );

            [LibraryImport(libName, EntryPoint = "GX_DecodeIA4")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_DecodeIA4_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] _in,
                [MarshalAs(UnmanagedType.U8)] ulong outSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] _out,
                in GXDecodeOptions_64 opt
            );

            [LibraryImport(libName, EntryPoint = "GX_DecodeIA8")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_DecodeIA8_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] _in,
                [MarshalAs(UnmanagedType.U8)] ulong outSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] _out,
                in GXDecodeOptions_64 opt
            );

            [LibraryImport(libName, EntryPoint = "GX_DecodeCI4")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_DecodeCI4_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] _in,
                [MarshalAs(UnmanagedType.U8)] ulong palSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] pal,
                [MarshalAs(UnmanagedType.U8)] ulong outSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 6)] uint[] _out,
                in GXDecodeOptions_64 opts
            );

            [LibraryImport(libName, EntryPoint = "GX_DecodeCI8")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_DecodeCI8_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] _in,
                [MarshalAs(UnmanagedType.U8)] ulong palSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] pal,
                [MarshalAs(UnmanagedType.U8)] ulong outSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 6)] uint[] _out,
                in GXDecodeOptions_64 opts
            );

            [LibraryImport(libName, EntryPoint = "GX_DecodeCI14X2")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_DecodeCI14X2_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] _in,
                [MarshalAs(UnmanagedType.U8)] ulong palSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] pal,
                [MarshalAs(UnmanagedType.U8)] ulong outSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 6)] uint[] _out,
                in GXDecodeOptions_64 opts
            );

            [LibraryImport(libName, EntryPoint = "GX_DecodeR5G6B5")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_DecodeR5G6B5_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] _in,
                [MarshalAs(UnmanagedType.U8)] ulong outSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] _out,
                in GXDecodeOptions_64 opt
            );

            [LibraryImport(libName, EntryPoint = "GX_DecodeRGB5A3")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_DecodeRGB5A3_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] _in,
                [MarshalAs(UnmanagedType.U8)] ulong outSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] _out,
                in GXDecodeOptions_64 opt
            );

            [LibraryImport(libName, EntryPoint = "GX_DecodeRGBA8")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_DecodeRGBA8_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] _in,
                [MarshalAs(UnmanagedType.U8)] ulong outSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] _out,
                in GXDecodeOptions_64 opt
            );

            [LibraryImport(libName, EntryPoint = "GX_DecodeCMP")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_DecodeCMP_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] _in,
                [MarshalAs(UnmanagedType.U8)] ulong outSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] _out,
                in GXDecodeOptions_64 opts
            );

            [LibraryImport(libName, EntryPoint = "GX_DecodePaletteIA8")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U1)]
            internal static partial bool GX_DecodePaletteI8_64(
                [MarshalAs(UnmanagedType.U8)] ulong palSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] ushort[] pal,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] uint[] palOut,
                in GXDecodeOptions_64 opts
            );

            [LibraryImport(libName, EntryPoint = "GX_DecodePaletteR5G6B5")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U1)]
            internal static partial bool GX_DecodePaletteR5G6B5_64(
                [MarshalAs(UnmanagedType.U8)] ulong palSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] ushort[] pal,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] uint[] palOut,
                in GXDecodeOptions_64 opts
            );

            [LibraryImport(libName, EntryPoint = "GX_DecodePaletteRGB5A3")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U1)]
            internal static partial bool GX_DecodePaletteRGB5A3_64(
                [MarshalAs(UnmanagedType.U8)] ulong palSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] ushort[] pal,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] uint[] palOut,
                in GXDecodeOptions_64 opts
            );

            [StructLayout(LayoutKind.Explicit, Size = 2, CharSet = CharSet.Ansi)]
            internal struct GXDecodeOptions_64
            {
                [FieldOffset(0)]
                [MarshalAs(UnmanagedType.U1)]
                internal bool flipX;

                [FieldOffset(1)]
                [MarshalAs(UnmanagedType.U1)]
                internal bool flipY;
            }



            // Encode
            [LibraryImport(libName, EntryPoint = "GX_EncodeI4")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_EncodeI4_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] _in,
                [MarshalAs(UnmanagedType.U8)] ulong outSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] _out,
                in GXEncodeOptions_64 opts
            );

            [LibraryImport(libName, EntryPoint = "GX_EncodeI8")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_EncodeI8_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] _in,
                [MarshalAs(UnmanagedType.U8)] ulong outSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] _out,
                in GXEncodeOptions_64 opts
            );

            [LibraryImport(libName, EntryPoint = "GX_EncodeIA4")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_EncodeIA4_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] _in,
                [MarshalAs(UnmanagedType.U8)] ulong outSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] _out,
                in GXEncodeOptions_64 opts
            );

            [LibraryImport(libName, EntryPoint = "GX_EncodeIA8")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_EncodeIA8_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] _in,
                [MarshalAs(UnmanagedType.U8)] ulong outSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] _out,
                in GXEncodeOptions_64 opts
            );

            [LibraryImport(libName, EntryPoint = "GX_EncodeCI4")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_EncodeCI4_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inIdxSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] inIdx,
                [MarshalAs(UnmanagedType.U8)] ulong palSz,
                [MarshalAs(UnmanagedType.U8)] ulong outIdxSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)] byte[] outIdx,
                in GXEncodeOptions_64 opts
            );

            [LibraryImport(libName, EntryPoint = "GX_EncodeCI8")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_EncodeCI8_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inIdxSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] inIdx,
                [MarshalAs(UnmanagedType.U8)] ulong palSz,
                [MarshalAs(UnmanagedType.U8)] ulong outIdxSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)] byte[] outIdx,
                in GXEncodeOptions_64 opts
            );

            [LibraryImport(libName, EntryPoint = "GX_EncodeCI14X2")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_EncodeCI14X2_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inIdxSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] inIdx,
                [MarshalAs(UnmanagedType.U8)] ulong palSz,
                [MarshalAs(UnmanagedType.U8)] ulong outIdxSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)] byte[] outIdx,
                in GXEncodeOptions_64 opts
            );

            [LibraryImport(libName, EntryPoint = "GX_EncodeR5G6B5")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_EncodeR5G6B5_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] _in,
                [MarshalAs(UnmanagedType.U8)] ulong outSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] _out,
                in GXEncodeOptions_64 opts
            );

            [LibraryImport(libName, EntryPoint = "GX_EncodeRGB5A3")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_EncodeRGB5A3_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] _in,
                [MarshalAs(UnmanagedType.U8)] ulong outSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] _out,
                in GXEncodeOptions_64 opts
            );

            [LibraryImport(libName, EntryPoint = "GX_EncodeRGBA8")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_EncodeRGBA8_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] _in,
                [MarshalAs(UnmanagedType.U8)] ulong outSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] _out,
                in GXEncodeOptions_64 opts
            );

            [LibraryImport(libName, EntryPoint = "GX_EncodeCMP")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U8)]
            internal static partial ulong GX_EncodeCMP_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] _in,
                [MarshalAs(UnmanagedType.U8)] ulong outSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] _out,
                in GXEncodeOptions_64 opts
            );

            [LibraryImport(libName, EntryPoint = "GX_EncodePaletteIA8")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U1)]
            internal static partial bool GX_EncodePaletteI8_64(
                [MarshalAs(UnmanagedType.U8)] ulong palSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] uint[] pal,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] ushort[] palOut,
                in GXEncodeOptions_64 opts
            );

            [LibraryImport(libName, EntryPoint = "GX_EncodePaletteR5G6B5")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U1)]
            internal static partial bool GX_EncodePaletteR5G6B5_64(
                [MarshalAs(UnmanagedType.U8)] ulong palSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] uint[] pal,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] ushort[] palOut,
                in GXEncodeOptions_64 opts
            );

            [LibraryImport(libName, EntryPoint = "GX_EncodePaletteRGB5A3")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U1)]
            internal static partial bool GX_EncodePaletteRGB5A3_64(
                [MarshalAs(UnmanagedType.U8)] ulong palSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] uint[] pal,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] ushort[] palOut,
                in GXEncodeOptions_64 opts
            );

            [LibraryImport(libName, EntryPoint = "GX_BuildPalette")]
            [UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
            [return: MarshalAs(UnmanagedType.U1)]
            internal static partial bool GX_BuildPalette_64(
                [MarshalAs(UnmanagedType.U2)] ushort w,
                [MarshalAs(UnmanagedType.U2)] ushort h,
                [MarshalAs(UnmanagedType.U8)] ulong inSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] _in,
                [MarshalAs(UnmanagedType.U8)] ulong palSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] uint[] pal,
                [MarshalAs(UnmanagedType.U8)] ulong outIdxSz,
                [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 6)] uint[] outIdx,
                [MarshalAs(UnmanagedType.U8)] out ulong outPalSz,
                in GXEncodeOptions_64 opts
            );

            internal enum GXAvgType_x86_64 : int
            {
                GX_AT_AVERAGE = 0,
                GX_AT_SQUARED,
                GX_AT_W3C,
                GX_AT_SRGB
            }

            internal enum GXDitherType_x86_x64 : int
            {
                GX_DT_THRESHOLD = 0,
                GX_DT_FLOYD_STEINBERG,
                GX_DT_ATKINSON,
                GX_DT_JARVIS_JUDICE_NINKE,
                GX_DT_STUCKI,
                GX_DT_BURKES,
                GX_DT_TWO_ROW_SIERRA,
                GX_DT_SIERRA,
                GX_DT_SIERRA_LITE
            }

            [StructLayout(LayoutKind.Explicit, Size = 30, CharSet = CharSet.Ansi)]
            internal unsafe struct GXEncodeOptions_64
            {
                [FieldOffset(0)]
                [MarshalAs(UnmanagedType.U1)]
                internal bool flipX;

                [FieldOffset(1)]
                [MarshalAs(UnmanagedType.U1)]
                internal bool flipY;

                [FieldOffset(2)]
                [MarshalAs(UnmanagedType.I4)]
                internal GXAvgType_x86_64 avgType;

                [FieldOffset(6)]
                [MarshalAs(UnmanagedType.I4)]
                internal GXDitherType_x86_x64 ditherType;

                [FieldOffset(10)]
                [MarshalAs(UnmanagedType.I4)]
                internal int squishFlags;

                [FieldOffset(14)]
                [MarshalAs(UnmanagedType.U8)]
                internal ulong squishMetricSz;

                // There should only be 3 components. However, squishMetricSz may say otherwise.
                // This could be an issue if squishMetricSz is anything other than 3.
                // In C# Marshaling, there is no way to specify the size of an array based on another member inside a
                // struct, so there is no way to handle this case from the marshaling side of the code. Instead, the
                // wrapper must handle it by ensuring squishMetricSz is 3 and squishMetric contains 3 elements.
                // Also, since LibraryImport source generator does not consider an array of bittable types of constant
                // size as bittable; one would have to make a custom marshaller or simply put a pointer and do the
                // marshalling at the call site.
                [FieldOffset(22)]
                [MarshalAs(UnmanagedType.U8)]
                internal float* squishMetric;
            }
        }
#pragma warning restore SYSLIB1092
#pragma warning restore IDE1006
#pragma warning restore CS8981
    }
}
