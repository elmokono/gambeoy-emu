using gameboy_rom_dissasembler.Structs;
using System.IO;
using System.Collections.Generic;
using System.Text;
//using System.Text.Json;
using Newtonsoft.Json;

namespace gameboy_rom_dissasembler.Parser
{
    internal class JSONParser
    {
        public OpcodesSet Parse(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<OpcodesSet>(jsonString);
        }

    }
}
