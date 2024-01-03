using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektFeladat
{
    internal class Konyv // Könyv osztály
    {
        public string title { get; set; } // Cím
        public string author { get; set; } // Szerző

        public string genre { get; set; } // Műfaj

        public int year { get; set; } // Kiadás éve

        public string url { get; set; } // Elérhetési URL
    }
    internal class Konyvek
    {
        public List<Konyv> books { get; set; } // List<Konyv>-et nem engedte a deszerializálásnál, ezért kellett ez.
    }
}
