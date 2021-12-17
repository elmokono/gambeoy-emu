using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace gameboy_rom_dissasembler.OpCodes
{
    internal class Z80OpCodesDissasembler
    {
        StringBuilder sb = new StringBuilder();

        public void Dissasemble(string fileName, int offset, Structs.OpcodesSet opcodesSet, Cartridge.CartridgeMetadata cartridgeMetadata, string outputFile)
        {
            sb.Clear();

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

                //Log($"{(binaryReader.BaseStream.Position - 1):X4}\t{((opCode[0] == 0xCB) ? "CB" : "--")}\t{currentOpCode.MNemonic}\t");
                Log($"{currentOpCode.MNemonic}\t");

                for (int i = 0; i < currentOpCode.Operands.OrderBy(x => x.Immediate).Count(); i++)
                {
                    if (i > 0) Log(",");
                    var nBytes = currentOpCode.Operands[i].Bytes;
                    if (nBytes == 0)
                    {
                        Log($" {currentOpCode.Operands[i].Name}", ConsoleColor.Green);
                        currentOpCode.Operands[i].Value = Encoding.ASCII.GetBytes(currentOpCode.Operands[i].Name);
                    }
                    else
                    {
                        currentOpCode.Operands[i].Value = binaryReader.ReadBytes(nBytes);
                        Log($" 0x{BitConverter.ToString(currentOpCode.Operands[i].Value).Replace("-", "")}", ConsoleColor.Yellow);
                    }
                }

                Log($"\t;0x{BitConverter.ToString(opCode)}", ConsoleColor.Gray, true);
            }

            File.WriteAllText(outputFile, sb.ToString());
        }

        void Log(string content, ConsoleColor consoleColor = ConsoleColor.Gray, bool breakLine = false)
        {
            //Console.ForegroundColor = consoleColor;
            //Console.Write(content);
            if (breakLine)
            {
                sb.AppendLine(content);
                //Console.WriteLine();
            }
            else
                sb.Append(content);
        }
    }
}
