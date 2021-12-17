using System;
using System.Collections.Generic;
using System.Text;

namespace gameboy_rom_dissasembler.Structs
{
    internal class Operand
    {
        public string Name { get; set; }
        public bool Immediate { get; set; }
        public int Bytes { get; set; }
    }
}
