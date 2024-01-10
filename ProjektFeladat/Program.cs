using ProjektFeladat;
using Spectre.Console;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Net.Security;
using System.Runtime.CompilerServices;
using YamlDotNet.Serialization;

AnsiConsole.MarkupLine("[bold turquoise2]Welcome![/]");
AnsiConsole.MarkupLine("[bold]type 'help' for all the commands[/]");
AnsiConsole.MarkupLine("[red]type 'exit' to exit.[/]");
KonyvReader kr = new(args[0]);
List<Konyv> Books = kr.LoadBooks();

// main loop
while (true)
{
    string Prompt = AnsiConsole.Prompt(new TextPrompt<string>("[bold]enter command: [/]"));
    String[] Splits = Prompt.Split(' ');
    CommandHandler Cmd = new();
    KonyvManager k = new();
    Command Type = Cmd.ConvertCmd(Splits[0]);
    switch (Type)
    {
        case Command.Search:
            Cmd.Search(string.Join(" ", Splits[1..]), Books);
            break;
        case Command.Add:
            if (Splits.Length <= 1) goto case Command.Help;
            Konyv book = new();
            try
            {
                book.title = Splits[1];
                book.author = Splits[2];
                book.genre = Splits[3];
                book.year = Int32.Parse(Splits[4]);
                book.url = Splits[5];

            }
            catch (Exception e)
            {
                AnsiConsole.MarkupLine($"[bold red1]invalid data, {e.Message}[/]");
                break;
            }

            k.AddBook(book, Books);
            break;

        case Command.Remove:
            if (Splits.Length <= 1) break;

            string BookName = string.Join(" ", Splits[1..]);
            var Match = Books.Find(new Predicate<Konyv>(book => book.title.Contains(BookName)));

            k.RemoveBook(Match, Books);
            break;

        case Command.Save:
            if (Splits.Length <= 1) goto case Command.Help;
            KonyvWriter kw = new(Splits[1]);
            kw.SaveBooks(Books);
            break;

        case Command.Exit:
            Environment.Exit(0);
            break;

        case Command.Help:
            if (Splits.Length >= 2)
            {
                AnsiConsole.MarkupLine(Cmd.Help(Cmd.ConvertCmd(Splits[1])));
            }
            else
            {
                AnsiConsole.MarkupLine(Cmd.Help(Command.Unknown));
            }
            break;
        case Command.Load:
            if (Splits.Length >= 2)
            {
                try
                {
                    Books = new KonyvReader(Splits[1]).LoadBooks();
                    AnsiConsole.MarkupLine($"[bold darkorange]'{Splits[1]}' has been reloaded[/]");
                }
                catch (Exception e)
                {
                    AnsiConsole.MarkupLine($"[bold red]failed to load library '{Splits[1]}'[/]");
                    AnsiConsole.MarkupLine($"[bold red]{e.Message}[/]");
                }
            }
            break;

        default:
            AnsiConsole.MarkupLine("[bold red] not implemented![/]");
            break;
    }
}
