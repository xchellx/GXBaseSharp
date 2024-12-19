using GXTextureSharp;

namespace BNRSharp.Serialization
{
    public static class BNRConstants
    {
        public const string MAGIC_BNR1 = "BNR1";

        public const string MAGIC_BNR2 = "BNR2";

        public const GXTexture.GXFormat IMAGE_FORMAT = GXTexture.GXFormat.RGB5A3;

        public const int IMAGE_WIDTH = 96;

        public const int IMAGE_HEIGHT = 32;

        public static readonly int IMAGE_SIZE = GXTexture.GetImageDataSize(GXTexture.GXFormat.RGB5A3, IMAGE_WIDTH, IMAGE_HEIGHT);

        public const int SHORT_TITLE_SIZE = 32;

        public const int SHORT_MAKER_SIZE = 32;

        public const int LONG_TITLE_SIZE = 64;

        public const int LONG_MAKER_SIZE = 64;

        public const int COMMENT_SIZE = 128;
    }
}
