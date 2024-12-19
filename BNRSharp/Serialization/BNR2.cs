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
    public sealed class BNR2 : Serializable, IBNR
    {
        /// <inheritdoc/>
        public byte[] Image { get; set; } = [];

        /// <summary>
        /// <b>NOTE: Code page 1252 is always used.</b>
        /// </summary>
        public BNRInfo EnglishInfo { get; set; }

        /// <summary>
        /// <b>NOTE: Code page 1252 is always used.</b>
        /// </summary>
        public BNRInfo GermanInfo { get; set; }

        /// <summary>
        /// <b>NOTE: Code page 1252 is always used.</b>
        /// </summary>
        public BNRInfo FrenchInfo { get; set; }

        /// <summary>
        /// <b>NOTE: Code page 1252 is always used.</b>
        /// </summary>
        public BNRInfo SpanishInfo { get; set; }

        /// <summary>
        /// <b>NOTE: Code page 1252 is always used.</b>
        /// </summary>
        public BNRInfo ItalianInfo { get; set; }

        /// <summary>
        /// <b>NOTE: Code page 1252 is always used.</b>
        /// </summary>
        public BNRInfo DutchInfo { get; set; }

        #region Serializable
        public BNR2(Serializable? parent = null) : base(parent)
        {
            EnglishInfo = new BNRInfo(this);
            GermanInfo = new BNRInfo(this);
            FrenchInfo = new BNRInfo(this);
            SpanishInfo = new BNRInfo(this);
            ItalianInfo = new BNRInfo(this);
            DutchInfo = new BNRInfo(this);
        }

        private readonly SerializationRequirements requirements = new(
            readNeedsSeek: false,
            writeNeedsSeek: false,
            validateReusableReader: r => r is EndianBinaryReader er && !er.IsLittleEndian && er.Encoding.Equals(IOUtil.Windows1252),
            validateReusableWriter: w => w is EndianBinaryWriter ew && !ew.IsLittleEndian && ew.Encoding.Equals(IOUtil.Windows1252),
            validateReadParent: p => p is BNR,
            validateWriteParent: p => p is BNR
        );

        public override ISerializationRequirements Requirements => requirements;

        protected override ISerializable? ReadImpl(Stream stream, BinaryReader? reusableReader, bool unfixedLen)
        {
            EndianBinaryReader reader = (EndianBinaryReader) reusableReader!;

            Image = reader.ReadBytes(IMAGE_SIZE);
            if (Image.Length == 0 || Image.Length != IMAGE_SIZE)
                throw new SerializationException(typeof(BNR2), "Invalid image data length");

            EnglishInfo.Read(stream, reusableReader, unfixedLen);

            GermanInfo.Read(stream, reusableReader, unfixedLen);

            FrenchInfo.Read(stream, reusableReader, unfixedLen);

            SpanishInfo.Read(stream, reusableReader, unfixedLen);

            ItalianInfo.Read(stream, reusableReader, unfixedLen);

            DutchInfo.Read(stream, reusableReader, unfixedLen);

            return null;
        }

        protected override void WriteImpl(Stream stream, BinaryWriter? reusableWriter, ISerializable? versionSpec,
            bool unfixedLen)
        {
            EndianBinaryWriter writer = (EndianBinaryWriter) reusableWriter!;

            if (Image.Length == 0 || Image.Length != IMAGE_SIZE)
                throw new SerializationException(typeof(BNR2), "Invalid image data length", true);
            writer.Write(Image);

            EnglishInfo.Write(stream, reusableWriter, versionSpec, unfixedLen);

            GermanInfo.Write(stream, reusableWriter, versionSpec, unfixedLen);

            FrenchInfo.Write(stream, reusableWriter, versionSpec, unfixedLen);

            SpanishInfo.Write(stream, reusableWriter, versionSpec, unfixedLen);

            ItalianInfo.Write(stream, reusableWriter, versionSpec, unfixedLen);

            DutchInfo.Write(stream, reusableWriter, versionSpec, unfixedLen);
        }

        protected override object GetMagicImpl(Stream stream, BinaryReader? reusableReader, bool unfixedLen)
            => throw new NotImplementedException();
        #endregion
    }
}
