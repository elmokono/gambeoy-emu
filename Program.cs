using gameboy_rom_dissasembler.Parser;
using gameboy_rom_dissasembler.Cartridge;
using System;

namespace gameboy_rom_dissasembler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var opCodes = new JSONParser().Parse(@"C:\WorkingFolderGITLab\gambeoy-emu\Resources\Opcodes.json");

            var romPath = @"C:\WorkingFolderGITLab\gambeoy-emu\Resources\snake.gb";

            var metadata = CartridgeMetadata.Parse(romPath, 0x100);

            var assemblerFile = romPath + ".asm";

            new OpCodes.Z80OpCodesDissasembler().Dissasemble(romPath, 0x150, opCodes, metadata, assemblerFile);
        }
    }
}
