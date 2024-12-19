namespace GXTextureSharp
{
    public static partial class GXTexture
    {
        public sealed class GXDecodeOptions(bool flipX = false, bool flipY = false)
        {
            internal gxtexture.GXDecodeOptions_64 raw = new()
            {
                flipX = flipX,
                flipY = flipY
            };

            public bool FlipX => raw.flipX;

            public bool FlipY => raw.flipY;
        }
    }
}
