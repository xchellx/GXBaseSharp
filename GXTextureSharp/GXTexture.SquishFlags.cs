using System;

namespace GXTextureSharp
{
    public static partial class GXTexture
    {
        [Flags]
        public enum SquishFlags : int
        {
            /// <summary>
            /// None.
            /// </summary>
            None = 0,
            /// <summary>
            /// Use DXT1 compression.
            /// </summary>
            DXT1 = (1 << 0),
            /// <summary>
            /// Use DXT3 compression.
            /// </summary>
            DXT3 = (1 << 1),
            /// <summary>
            /// Use DXT5 compression.
            /// </summary>
            DXT5 = (1 << 2),
            /// <summary>
            /// Use BC4 compression.
            /// </summary>
            BC4 = (1 << 3),
            /// <summary>
            /// Use BC5 compression.
            /// </summary>
            BC5 = (1 << 4),
            /// <summary>
            /// Use a slow but high quality colour compressor (the default).
            /// </summary>
            ColourClusterFit = (1 << 5),
            /// <summary>
            /// Use a fast but low quality colour compressor.
            /// </summary>
            ColourRangeFit = (1 << 6),
            /// <summary>
            /// Weight the colour by alpha during cluster fit (disabled by default).
            /// </summary>
            WeightColourByAlpha = (1 << 7),
            /// <summary>
            /// Use a very slow but very high quality colour compressor.
            /// </summary>
            ColourIterativeClusterFit = (1 << 8),
            /// <summary>
            /// Source is BGRA rather than RGBA
            /// </summary>
            SourceBGRA = (1 << 9)
        }
    }
}
