namespace GXTextureSharp
{
    public static partial class GXTexture
    {
        public enum GXPaletteFormat : int
        {
            /// <summary>
            /// Intensity (8 bits intensity)
            /// </summary>
            I8 = 0,
            /// <summary>
            /// RGB (5 bits RB, 6 bits G)
            /// </summary>
            R5G6B5,
            /// <summary>
            /// RGBA (1 bit mode, mode = 1: 5 bits RGB, mode = 0: 4 bits RGB, 3 bits alpha)
            /// </summary>
            RGB5A3
        }
    }
}
