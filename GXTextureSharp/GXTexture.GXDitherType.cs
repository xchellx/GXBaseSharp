namespace GXTextureSharp
{
    public static partial class GXTexture
    {
        public enum GXDitherType : int
        {
            Threshold = gxtexture.GXDitherType_x86_x64.GX_DT_THRESHOLD,
            FloydSteinberg = gxtexture.GXDitherType_x86_x64.GX_DT_FLOYD_STEINBERG,
            Atkinson = gxtexture.GXDitherType_x86_x64.GX_DT_ATKINSON,
            JarvisJudiceNinke = gxtexture.GXDitherType_x86_x64.GX_DT_JARVIS_JUDICE_NINKE,
            Stucki = gxtexture.GXDitherType_x86_x64.GX_DT_STUCKI,
            Burkes = gxtexture.GXDitherType_x86_x64.GX_DT_BURKES,
            TwoRowSierra = gxtexture.GXDitherType_x86_x64.GX_DT_TWO_ROW_SIERRA,
            Sierra = gxtexture.GXDitherType_x86_x64.GX_DT_SIERRA,
            SierraLite = gxtexture.GXDitherType_x86_x64.GX_DT_SIERRA_LITE
        }
    }
}
