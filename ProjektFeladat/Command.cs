using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace ProjektFeladat
{
    internal enum Commands
    {
        None,
        Search,
        Add,
        Remove,
    };
    internal class Command
    {
        internal Commands ConvertCmd(string command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            if (command[1] == 'q') return Commands.Search; // 'q' to Search
            else if (command[1] == 'a') return Commands.Add; // 'a' to Add
            else if (command[1] == 'r') return Commands.Remove; // 'r' to Remove
            else return Commands.None; // None to anything else.
        }
        internal void Search(Konyvek books) { }
        internal void Remove(Konyv book) { }
        internal void Add(Konyv book) { }
        internal void Save() { }
    }
}
