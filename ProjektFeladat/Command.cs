using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektFeladat
{
    internal class Command
    {
        public enum Commands
        {
            None,
            Search,
            Add,
            Remove,
        };
        public Command() { }
    }
}
