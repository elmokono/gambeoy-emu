using System.IO;
using System.Text;
using System;

namespace gameboy_rom_dissasembler.Cartridge
{
    internal class CartridgeMetadata
    {
        public byte[] EntryPoint { get; set; }
        public byte[] NintendoLogo { get; set; }
        public string Title { get; set; }
        public byte CGB { get; set; }
        public string NewLicenseeCode { get; set; }
        public byte SGB { get; set; }
        public byte CartridgeType { get; set; }
        public byte RomSize { get; set; }
        public byte RamSize { get; set; }
        public byte DestinationCode { get; set; }
        public byte OldLicenseeCode { get; set; }
        public byte MaskRomVersion { get; set; }
        public byte HeaderCheckSum { get; set; }
        public ushort GlobalCheckSum { get; set; }

        public static CartridgeMetadata Parse(string fileName, int offset)
        {
            using var binaryReader = new BinaryReader(File.Open(fileName, FileMode.Open));
            binaryReader.BaseStream.Seek(offset, SeekOrigin.Begin);

            var metadata = new CartridgeMetadata
            {
                EntryPoint = binaryReader.ReadBytes(4),
                NintendoLogo = binaryReader.ReadBytes(48),
                Title = Encoding.Default.GetString(binaryReader.ReadBytes(15)),
                CGB = binaryReader.ReadByte(),
                NewLicenseeCode = Encoding.Default.GetString(binaryReader.ReadBytes(2)),
                SGB = binaryReader.ReadByte(),
                CartridgeType = binaryReader.ReadByte(),
                RomSize = binaryReader.ReadByte(),
                RamSize = binaryReader.ReadByte(),
                DestinationCode = binaryReader.ReadByte(),
                OldLicenseeCode = binaryReader.ReadByte(),
                MaskRomVersion = binaryReader.ReadByte(),
                HeaderCheckSum = binaryReader.ReadByte(),
                GlobalCheckSum = binaryReader.ReadUInt16()
            };

            binaryReader.Close();

            //header checksum verification
            ChecksumVerification(fileName, offset + 4 + 48, metadata.HeaderCheckSum);

            return metadata;
        }

        static void ChecksumVerification(string fileName, int offset, ushort headerCheckSum)
        {
            using var binaryReader = new BinaryReader(File.Open(fileName, FileMode.Open));
            binaryReader.BaseStream.Seek(offset, SeekOrigin.Begin); //skips entry point and nintendo logo

            int checkSum = 0;
            for (int i = 0; i < 25; i++)
                checkSum = checkSum - binaryReader.ReadByte() - 1;

            if (BitConverter.GetBytes(checkSum)[0] != headerCheckSum)
                throw new Exception("Invalid header checksum");
        }
    }
}
