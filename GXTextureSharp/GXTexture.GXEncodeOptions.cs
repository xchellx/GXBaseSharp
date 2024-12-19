using System;
using System.Linq;
using UtilSharp.Extensions;

namespace GXTextureSharp
{
    public static partial class GXTexture
    {
        public sealed class GXEncodeOptions(bool flipX = false, bool flipY = false, GXAverageType avgType = GXAverageType.Average, GXDitherType ditherType = GXDitherType.Threshold, SquishFlags squishFlags = SquishFlags.None, float[]? squishMetric = null)
        {
            internal gxtexture.GXEncodeOptions_64 raw = new()
            {
                flipX = flipX,
                flipY = flipY,
                avgType = CheckEnum<gxtexture.GXAvgType_x86_64, GXAverageType>(avgType, nameof(avgType)),
                ditherType = CheckEnum<gxtexture.GXDitherType_x86_x64, GXDitherType>(ditherType, nameof(ditherType)),
                squishFlags = CheckAndFixFlags(squishFlags, nameof(SquishFlags)),
                squishMetricSz = 3,
                squishMetric = null
            };

            internal readonly float[]? squishMetric = CheckSquishMetric(squishMetric, nameof(squishMetric));

            public bool FlipX => raw.flipX;

            public bool FlipY => raw.flipY;

            public GXAverageType AverageType => (GXAverageType) raw.avgType;

            public GXDitherType DitherType => (GXDitherType) raw.ditherType;

            public SquishFlags SquishFlags => (SquishFlags) raw.squishFlags;

            public float SquishMetric1 => squishMetric?[0] ?? 1.0f;

            public float SquishMetric2 => squishMetric?[1] ?? 1.0f;

            public float SquishMetric3 => squishMetric?[2] ?? 1.0f;

            private static R CheckEnum<R, T>(T enu, string paramName)
                where R : struct, Enum
                where T : struct, Enum
            {
                if (!Enum.IsDefined<T>(enu))
                    throw new ArgumentException($"Invalid {typeof(T).Name}", paramName);

                return (R) (object) enu;
            }

            private static int CheckAndFixFlags(SquishFlags flags, string paramName)
            {
                if (!flags.IsFlagsDefined())
                    throw new ArgumentException("Invalid flags", paramName);

                flags = SquishFlags.DXT1 | (flags & ~(SquishFlags.DXT1 | SquishFlags.DXT3 | SquishFlags.DXT5 |
                    SquishFlags.BC4 | SquishFlags.BC5 | SquishFlags.SourceBGRA));

                if (
                    (flags & SquishFlags.ColourClusterFit) != SquishFlags.ColourClusterFit
                    && (flags & SquishFlags.ColourIterativeClusterFit) != SquishFlags.ColourIterativeClusterFit
                    && (flags & SquishFlags.ColourRangeFit) != SquishFlags.ColourRangeFit
                )
                    flags |= SquishFlags.ColourClusterFit;

                return (int) flags;
            }

            private static float[]? CheckSquishMetric(float[]? squishMetric, string paramName)
            {
                if (squishMetric == null)
                    return squishMetric;
                else if (squishMetric.Length != 3)
                    throw new ArgumentException("Must be of length 3", paramName);
                else if (squishMetric.Any(f => f < 0.0f || f > 1.0f))
                    throw new ArgumentException("Must be within the range of 0.0 to 1.0", paramName);
                else
                    return squishMetric;
            }
        }
    }
}
