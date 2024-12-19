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
using System.Linq;
using static BNRSharp.Serialization.BNRConstants;
using System;

namespace BNRSharp.Serialization
{
    public sealed class BNRInfo : Serializable
    {
        /// <summary>
        /// Max size is 32 bytes.
        /// </summary>
        public string ShortTitle { get; set; } = string.Empty;

        /// <summary>
        /// Max size is 32 bytes.
        /// </summary>
        public string ShortMaker { get; set; } = string.Empty;

        /// <summary>
        /// Max size is 64 bytes.
        /// </summary>
        public string LongTitle { get; set; } = string.Empty;

        /// <summary>
        /// Max size is 64 bytes.
        /// </summary>
        public string LongMaker { get; set; } = string.Empty;

        /// <summary>
        /// Max size is 128 bytes.<br/>
        /// Can be two lines. Second line is indicated by LF (0x0A).
        /// </summary>
        public string Comment { get; set; } = string.Empty;

        #region Serializable
        public BNRInfo(Serializable? parent = null) : base(parent)
        {
        }

        private readonly SerializationRequirements requirements = new(
            readNeedsSeek: false,
            writeNeedsSeek: false,
            validateReusableReader: r => r is EndianBinaryReader er && !er.IsLittleEndian && (er.Encoding.Equals(IOUtil.Windows1252) || er.Encoding.Equals(IOUtil.ShiftJIS)),
            validateReusableWriter: w => w is EndianBinaryWriter ew && !ew.IsLittleEndian && (ew.Encoding.Equals(IOUtil.Windows1252) || ew.Encoding.Equals(IOUtil.ShiftJIS)),
            validateReadParent: p => p is BNR1 || p is BNR2,
            validateWriteParent: p => p is BNR1 || p is BNR2
        );

        public override ISerializationRequirements Requirements => requirements;

        protected override ISerializable? ReadImpl(Stream stream, BinaryReader? reusableReader, bool unfixedLen)
        {
            EndianBinaryReader reader = (EndianBinaryReader) reusableReader!;
            int strLen;

            ShortTitle = reader.ReadCString(nullTerm: true, seekBackOnNull: true, byteLen: SHORT_TITLE_SIZE);
            strLen = reader.Encoding.GetByteCount(ShortTitle);
            reader.Seek(SHORT_TITLE_SIZE - strLen, SeekOrigin.Current);

            ShortMaker = reader.ReadCString(nullTerm: true, seekBackOnNull: true, byteLen: SHORT_MAKER_SIZE);
            strLen = reader.Encoding.GetByteCount(ShortMaker);
            reader.Seek(SHORT_MAKER_SIZE - strLen, SeekOrigin.Current);

            LongTitle = reader.ReadCString(nullTerm: true, seekBackOnNull: true, byteLen: LONG_TITLE_SIZE);
            strLen = reader.Encoding.GetByteCount(LongTitle);
            reader.Seek(LONG_TITLE_SIZE - strLen, SeekOrigin.Current);

            LongMaker = reader.ReadCString(nullTerm: true, seekBackOnNull: true, byteLen: LONG_MAKER_SIZE);
            strLen = reader.Encoding.GetByteCount(LongMaker);
            reader.Seek(LONG_MAKER_SIZE - strLen, SeekOrigin.Current);

            Comment = reader.ReadCString(nullTerm: true, seekBackOnNull: true, byteLen: COMMENT_SIZE);
            strLen = reader.Encoding.GetByteCount(Comment);
            reader.Seek(COMMENT_SIZE - strLen, SeekOrigin.Current);

            return null;
        }

        protected override void WriteImpl(Stream stream, BinaryWriter? reusableWriter, ISerializable? versionSpec,
            bool unfixedLen)
        {
            EndianBinaryWriter writer = (EndianBinaryWriter) reusableWriter!;
            int strLen;

            strLen = writer.Encoding.GetByteCount(ShortTitle);
            if (strLen > SHORT_TITLE_SIZE)
                throw new SerializationException(typeof(BNRInfo), "Invalid short title", true);
            writer.Write(ShortTitle, AW_CS._, false);
            writer.Write(Enumerable.Repeat((byte) 0, SHORT_TITLE_SIZE - strLen).ToArray());

            strLen = writer.Encoding.GetByteCount(ShortMaker);
            if (strLen > SHORT_MAKER_SIZE)
                throw new SerializationException(typeof(BNRInfo), "Invalid short maker", true);
            writer.Write(ShortMaker, AW_CS._, false);
            writer.Write(Enumerable.Repeat((byte) 0, SHORT_MAKER_SIZE - strLen).ToArray());

            strLen = writer.Encoding.GetByteCount(LongTitle);
            if (strLen > LONG_TITLE_SIZE)
                throw new SerializationException(typeof(BNRInfo), "Invalid long title", true);
            writer.Write(LongTitle, AW_CS._, false);
            writer.Write(Enumerable.Repeat((byte) 0, LONG_TITLE_SIZE - strLen).ToArray());

            strLen = writer.Encoding.GetByteCount(LongMaker);
            if (strLen > LONG_MAKER_SIZE)
                throw new SerializationException(typeof(BNRInfo), "Invalid long maker", true);
            writer.Write(LongMaker, AW_CS._, false);
            writer.Write(Enumerable.Repeat((byte) 0, LONG_MAKER_SIZE - strLen).ToArray());

            strLen = writer.Encoding.GetByteCount(Comment);
            if (strLen > COMMENT_SIZE)
                throw new SerializationException(typeof(BNRInfo), "Invalid comment", true);
            writer.Write(Comment, AW_CS._, false);
            writer.Write(Enumerable.Repeat((byte) 0, COMMENT_SIZE - strLen).ToArray());
        }

        protected override object GetMagicImpl(Stream stream, BinaryReader? reusableReader, bool unfixedLen)
            => throw new NotImplementedException();
        #endregion
    }
}
