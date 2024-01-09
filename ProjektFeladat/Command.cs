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
            switch (command) // TODO
            {
                case Command.Search:
                    return "help: search | q\n" +
                        "searching is done via the 'search [[query]]' command\n" +
                        "looks for the first occurence of the supplied query,\n" +
                        "run the command without any arguments and it shall return the whole library.";
                case Command.Add:
                    return "help: add | a\n" +
                           "adding books to the library is done via the 'add [[book_data]]' command\n" +
                           "[bold]arguments:[/]\n" +
                           "[[title]]:  string\n" +
                           "[[author]]: string\n" +
                           "[[genre]]:  string\n" +
                           "[[year]]:   integer\n" +
                           "[[url]]:    string\n" +
                           "[gold1 bold]:warning: NOTE: this does not save the added book to disk, you still need to run the 'save' command.[/]";
                case Command.Remove:
                    return "help: remove | r\n" +
                           "removing books from the library is done via the 'remove [[book_name]]' command\n" +
                           "it functions similarly to search, in the sense that it deletes the first occurrence\n" +
                           "of a book that contains [[book_name]] in the title\n" +
                           "[gold1 bold]:warning: NOTE: this does not save the added book to disk, you still need to run the 'save' command.[/]";
                case Command.Exit:
                    return "help: exit\n" +
                            "exits the program. same result can be achieved with CTRL+C.";
                case Command.Save:
                    return "help: save\n" +
                           "saves the library into [[file]]" +
                           "the file type should preferably be YAML.";
                case Command.Unknown:
                    goto default;
                default:
                    return "usage: konyvtar [[file]]\n" +
                           "[underline]currently available list of commands:[/]\n" +
                           "search   | q [[query]]\n" +
                           "add      | a [[book_data]]\n" +
                           "remove   | r [[book_name]]\n" +
                           "exit\n" +
                           "save\n" +
                           "\n[bold]to learn more about a command, use [/]'help [[command]]'";
            }
        }
    }
}
