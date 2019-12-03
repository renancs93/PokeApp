using System;
using System.Collections.Generic;
using System.Text;

namespace PokeApp.Model
{
    class RootObject
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<Pokemon> Results { get; set; }
    }
}
