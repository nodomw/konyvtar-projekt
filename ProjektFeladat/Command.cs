using Spectre.Console;
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
        Exit,
        Save
    };
    internal class Command
    {
        internal Commands ConvertCmd(string command) // does not run the commands but converts them to be able to be used in a switch statement
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            if (command == "q") return Commands.Search; // 'q' to Search
            else if (command == "a") return Commands.Add; // 'a' to Add
            else if (command == "r") return Commands.Remove; // 'r' to Remove
            else if (command == "exit") return Commands.Exit;
            else if (command == "save") return Commands.Save;
            else return Commands.None; // return not implemented
        }
        internal void Search(string query, List<Konyv> Books)
        {
            // tábla GUI előkészítése
            var table = new Table();

            table.AddColumn("Title");
            table.AddColumn("Year");
            table.AddColumn("Author");
            table.AddColumn("Genre");
            table.AddColumn("URL");
            table.Border(TableBorder.Rounded);

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].title.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0) // indexes keresés a 'search' változó alapján, nagybetűket ignorálva. a keresés csak a cím alapján működik.
                {
                    var konyv = Books[i]; // Jelenlegi könyv változó

                    // egy sor hozzáadása az i-edik könyvvel
                    table.AddRow(konyv.title, konyv.year.ToString(), konyv.author, konyv.genre, $"[blue underline]{konyv.url}[/]");
                }
            }

            AnsiConsole.Write(table); // tábla kiírása
        }
        internal void Remove(Konyv book)
        {
            // TODO
        }
        internal void Add(Konyv book)
        {
            // TODO
        }
        internal void Save()
        {
            // TODO
        }
    }
}
