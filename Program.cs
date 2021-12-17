using gameboy_rom_dissasembler.Parser;
using gameboy_rom_dissasembler.Structs;
using System;

namespace gameboy_rom_dissasembler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var opCodes = new JSONParser().Parse(@"C:\WorkingFolderGITLab\gameboy-emu\dissasembler-csharp\gameboy-rom-dissasembler\Resources\Opcodes.json");

            var metadata = CartridgeMetadata.Parse(
                @"C:\WorkingFolderGITLab\gameboy-emu\dissasembler-csharp\gameboy-rom-dissasembler\Resources\snake.gb",
                0x100
            );
        }
    }
}
