using System;
using System.Collections.Generic;
using System.Text;

namespace gameboy_rom_dissasembler.Structs
{
    /// <summary>
    /// https://gbdev.io/pandocs/CPU_Registers_and_Flags.html
    /// </summary>
    internal class Flags
    {
        public string Z { get; set; } //Zero flag
        public string N { get; set; } //Subtraction flag (BCD)
        public string H { get; set; } //Half Carry flag (BCD)
        public string C { get; set; } //Carry flag

        public override string ToString()
        {
            return $"Z:{Z}, N:{N}, H:{H}, C:{C}";
        }
    }
}
