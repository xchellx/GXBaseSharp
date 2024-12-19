namespace GXTextureSharp
{
    public static partial class GXTexture
    {
        public enum GXFormat : int
        {
            /// <summary>
            /// Intensity (4 bits intensity)
            /// </summary>
            I4 = 0,
            /// <summary>
            /// Intensity (8 bits intensity)
            /// </summary>
            I8,
            /// <summary>
            /// Intensity Alpha (4 bits intensity, 4 bits alpha)
            /// </summary>
            IA4,
            /// <summary>
            /// Intensity Alpha (8 bits intensity, 8 bits alpha)
            /// </summary>
            IA8,
            /// <summary>
            /// Color Index (4 bits color index)
            /// </summary>
            CI4,
            /// <summary>
            /// Color Index (8 bits color index)
            /// </summary>
            CI8,
            /// <summary>
            /// Color Index (2 bits ignored, 14 bits color index)
            /// </summary>
            CI14X2,
            /// <summary>
            /// RGB (5 bits RB, 6 bits G)
            /// </summary>
            R5G6B5,
            /// <summary>
            /// RGBA (1 bit mode, mode = 1: 5 bits RGB, mode = 0: 4 bits RGB, 3 bits alpha)
            /// </summary>
            RGB5A3,
            /// <summary>
            /// RGBA (2 groups, group 1: 8 bits AR, group 2: 8 bits GB)
            /// </summary>
            RGBA8,
            /// <summary>
            /// Compressed (8 bytes DXT1 block)
            /// </summary>
            CMP
        }
    }
}
