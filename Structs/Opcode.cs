using System;
using System.Collections.Generic;
using System.Text;

namespace gameboy_rom_dissasembler.Structs
{
    internal class OpcodesSet
    {
        public Dictionary<string, Opcode> UnPrefixed { get; set; }
        public Dictionary<string, Opcode> CBPrefixed { get; set; }
    }

    internal class Opcode
    {
        public string MNemonic { get; set; }
        public int Bytes { get; set; }
        public int[] Cycles { get; set; }
        public Operand[] Operands { get; set; }
        public Flags Flags { get; set; }
    }
}
