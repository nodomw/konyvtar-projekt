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
    internal enum Command // Parancs-típus enumerátorok
    {
        Search, //      Keresés
        Add, //         Hozzáadás
        Remove, //      Eltávolítás
        Exit, //        Kilépés
        Save, //        Mentés
        Help, //        Súgó
        Unknown //      Ismeretlen => nem implementált
    };
    internal class CommandHandler
    {
        internal Command ConvertCmd(string command) // does not run the commands but converts them to be able to be used in a switch statement
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            // i know hard coded aliases suck but it is whaht it is
            if (command == "q" || command == "search") return Command.Search;
            else if (command == "a" || command == "add") return Command.Add;
            else if (command == "r" || command == "remove") return Command.Remove;
            else if (command == "exit") return Command.Exit;
            else if (command == "save") return Command.Save;
            else if (command == "help") return Command.Help;
            else return Command.Unknown;
        }
        internal void Search(string query, List<Konyv> Books)
        {
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
                    var Book = Books[i];

                    table.AddRow(Book.title, Book.year.ToString(), Book.author, Book.genre, $"[blue underline]{Book.url}[/]");
                }
            }

            AnsiConsole.Write(table); // tábla kiírása
        }
        internal string Help(Command command) // Súgó
        {
            switch (command) // TODO - Dávid
            {
                case Command.Search:
                    return @"HELP: Querying elements";
                case Command.Add:
                    return @"HELP: Adding elements";
                case Command.Remove:
                    return @"HELP: Removing elements";
                case Command.Exit:
                    return @"HELP: Exit";
                case Command.Save:
                    return @"HELP: Save";
                case Command.Help:
                    return @"HELP: Help";
                case Command.Unknown:
                    goto default;
                default:
                    return @"HELP: ";
            }
        }
    }
}
