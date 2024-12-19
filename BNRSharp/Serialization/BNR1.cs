/*
 * MIT License
 * 
 * Copyright (c) 2023 Yonder
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using SerializableSharp;
using UtilSharp.IO;
using UtilSharp.Util;
using System.IO;
using static BNRSharp.Serialization.BNRConstants;
using System;

namespace BNRSharp.Serialization
{
    public sealed class BNR1 : Serializable, IBNR
    {
        /// <inheritdoc/>
        public byte[] Image { get; set; } = [];

        /// <summary>
        /// <b>NOTE: If this BNR was made for a Japanese disc, then code page 932 is used. Otherwise, code page 1252 is
        /// used.</b>
        /// </summary>
        public BNRInfo EnglishOrJapaneseInfo { get; set; }

        #region Serializable
        public BNR1(Serializable? parent = null) : base(parent)
        {
            EnglishOrJapaneseInfo = new BNRInfo(this);
        }

        private readonly SerializationRequirements requirements = new(
            readNeedsSeek: false,
            writeNeedsSeek: false,
            validateReusableReader: r => r is EndianBinaryReader er && !er.IsLittleEndian && (er.Encoding.Equals(IOUtil.Windows1252) || er.Encoding.Equals(IOUtil.ShiftJIS)),
            validateReusableWriter: w => w is EndianBinaryWriter ew && !ew.IsLittleEndian && (ew.Encoding.Equals(IOUtil.Windows1252) || ew.Encoding.Equals(IOUtil.ShiftJIS)),
            validateReadParent: p => p is BNR,
            validateWriteParent: p => p is BNR
        );

        public override ISerializationRequirements Requirements => requirements;

        protected override ISerializable? ReadImpl(Stream stream, BinaryReader? reusableReader, bool unfixedLen)
        {
            EndianBinaryReader reader = (EndianBinaryReader) reusableReader!;

            Image = reader.ReadBytes(IMAGE_SIZE);
            if (Image.Length == 0 || Image.Length != IMAGE_SIZE)
                throw new SerializationException(typeof(BNR1), "Invalid image data length");

            EnglishOrJapaneseInfo.Read(stream, reusableReader, unfixedLen);

            return null;
        }

        protected override void WriteImpl(Stream stream, BinaryWriter? reusableWriter, ISerializable? versionSpec,
            bool unfixedLen)
        {
            EndianBinaryWriter writer = (EndianBinaryWriter) reusableWriter!;

            if (Image.Length == 0 || Image.Length != IMAGE_SIZE)
                throw new SerializationException(typeof(BNR1), "Invalid image data length", true);
            writer.Write(Image);

            EnglishOrJapaneseInfo.Write(stream, reusableWriter, versionSpec, unfixedLen);
        }

        protected override object GetMagicImpl(Stream stream, BinaryReader? reusableReader, bool unfixedLen)
            => throw new NotImplementedException();
        #endregion
    }
}
