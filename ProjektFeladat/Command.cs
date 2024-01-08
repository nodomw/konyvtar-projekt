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
        Search,
        Add,
        Remove,
        Exit,
        Save,
        Help,
        Unknown
    };
    internal class Command
    {
        internal Commands ConvertCmd(string command) // does not run the commands but converts them to be able to be used in a switch statement
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            // i know hard coded aliases suck but it is whaht it is
            if (command == "q" || command == "search") return Commands.Search;
            else if (command == "a" || command == "add") return Commands.Add;
            else if (command == "r" || command == "remove") return Commands.Remove;
            else if (command == "exit") return Commands.Exit;
            else if (command == "save") return Commands.Save;
            else if (command == "help") return Commands.Help;
            else return Commands.Unknown;
            // else return Commands.Exit; // return not implemented
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
        internal string Help(Commands command)
        {

            switch (command)
            {
                case Commands.Search:
                    return @"HELP: Querying elements";
                case Commands.Add:
                    return @"HELP: Adding elements";
                case Commands.Remove:
                    return @"HELP: Removing elements";
                case Commands.Exit:
                    return @"HELP: Exit";
                case Commands.Save:
                    return @"HELP: Save";
                case Commands.Help:
                    return @"HELP: Help";
                case Commands.Unknown:
                    goto default;
                default:
                    return @"HELP: ";
            }
        }
    }
}
