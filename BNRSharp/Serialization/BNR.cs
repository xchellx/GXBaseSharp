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

namespace BNRSharp.Serialization
{
    public sealed class BNR : Serializable
    {
        public string Magic { get; set; } = "";

        public SerializationLabel Padding { get; } = new("32 byte aligned padding");

        #region Serializable
        public BNR(Serializable? parent = null) : base(parent)
        {
        }

        private readonly SerializationRequirements requirements = new(
            readNeedsSeek: true,
            writeNeedsSeek: true,
            validateReusableReader: r => r is EndianBinaryReader er && !er.IsLittleEndian && (er.Encoding.Equals(IOUtil.Windows1252) || er.Encoding.Equals(IOUtil.ShiftJIS)),
            validateReusableWriter: w => w is EndianBinaryWriter ew && !ew.IsLittleEndian && (ew.Encoding.Equals(IOUtil.Windows1252) || ew.Encoding.Equals(IOUtil.ShiftJIS)),
            validateVersionSpec: v => v is BNR1 || v is BNR2
        );

        public override ISerializationRequirements Requirements => requirements;

        protected override ISerializable? ReadImpl(Stream stream, BinaryReader? reusableReader, bool unfixedLen)
        {
            EndianBinaryReader reader = (EndianBinaryReader) reusableReader!;

            Magic = reader.ReadFourCC();
            if (Magic != MAGIC_BNR1 && Magic != MAGIC_BNR2)
                throw new SerializationException(typeof(BNR), "Invalid magic");

            reader.Align(32);

            return Magic == MAGIC_BNR2 ? new BNR2(this) : new BNR1(this);
        }

        protected override void WriteImpl(Stream stream, BinaryWriter? reusableWriter, ISerializable? versionSpec,
            bool unfixedLen)
        {
            EndianBinaryWriter writer = (EndianBinaryWriter) reusableWriter!;

            if (Magic != MAGIC_BNR1 && Magic != MAGIC_BNR2)
                throw new SerializationException(typeof(BNR), "Invalid magic");
            writer.Write(Magic, AW_FC._);

            writer.Align(32);

            versionSpec?.Write(stream, reusableWriter, versionSpec, unfixedLen);
        }

        protected override object GetMagicImpl(Stream stream, BinaryReader? reusableReader, bool unfixedLen)
            => ((EndianBinaryReader) reusableReader!).ReadFourCC();
        #endregion
    }
}
