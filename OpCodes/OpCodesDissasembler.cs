using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace gameboy_rom_dissasembler.OpCodes
{
    internal class OpCodesDissasembler
    {
        public static void Dissasemble(string fileName, int offset, Structs.OpcodesSet opcodesSet, Cartridge.CartridgeMetadata cartridgeMetadata)
        {
            using var binaryReader = new BinaryReader(File.Open(fileName, FileMode.Open, FileAccess.Read));
            binaryReader.BaseStream.Seek(offset, SeekOrigin.Begin);

            Structs.Opcode currentOpCode;
            while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                var opCode = binaryReader.ReadBytes(1);

                if (opCode[0] == 0xCB)
                    currentOpCode = opcodesSet.CBPrefixed[$"0x{BitConverter.ToString(opCode)}"];
                else
                    currentOpCode = opcodesSet.UnPrefixed[$"0x{BitConverter.ToString(opCode)}"];
                                
                Console.Write($"{(binaryReader.BaseStream.Position - 1):X4}\t{((opCode[0] == 0xCB) ? "CB" : "--")}\t{currentOpCode.MNemonic}\t");
                
                for (int i = 0; i < currentOpCode.Operands.OrderBy(x => x.Immediate).Count(); i++)
                {
                    if (i > 0) Console.Write(",");
                    var nBytes = currentOpCode.Operands[i].Bytes;
                    if (nBytes == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($" {currentOpCode.Operands[i].Name}");
                        currentOpCode.Operands[i].Value = Encoding.ASCII.GetBytes(currentOpCode.Operands[i].Name);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        currentOpCode.Operands[i].Value = binaryReader.ReadBytes(nBytes);
                        Console.Write($" 0x{BitConverter.ToString(currentOpCode.Operands[i].Value).Replace("-", "")}");
                    }
                    
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}
