using SerializableSharp;

namespace BNRSharp.Serialization
{
    public interface IBNR : ISerializable
    {
        /// <summary>
        /// A 96x32 RGB5A3 GX encoded image.
        /// </summary>
        public byte[] Image { get; set; }
    }
}
