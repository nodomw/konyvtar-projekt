using ProjektFeladat;
using Spectre.Console;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using YamlDotNet.Serialization;

AnsiConsole.MarkupLine("[bold yellow]Welcome![/]");
AnsiConsole.MarkupLine("[red]Type 'exit' to exit.[/]");
Console.WriteLine("");
AnsiConsole.MarkupLine("[bold white]COMMANDS:[/]");
Console.WriteLine("type 'q [query]' to search");
Console.WriteLine("type 'a [book_title] [book_author] [book_genre] [book_url]' to add a book.");
Console.WriteLine("type 'r [query]' to delete a book.");

List<Konyv> Books = new KonyvReader().LoadBooks(args[0]);

// main loop
while (true)
{
    string Command = AnsiConsole.Prompt(new TextPrompt<string>("[bold]enter command: [/]"));

    String[] Splits = Command.Split(' ');
    CommandHandler Cmd = new();
    Command Type = Cmd.ConvertCmd(Splits[0]);
    
    switch (Type)
    {
        case ProjektFeladat.Command.Search:
            Cmd.Search(string.Join(" ", Splits[1..]), Books);
            break;
        case ProjektFeladat.Command.Add: // TODO - Dávid
            goto default;
        case ProjektFeladat.Command.Remove: // TODO - Dávid
            goto default;
        case ProjektFeladat.Command.Save: // TODO - Dávid
            goto default;
        case ProjektFeladat.Command.Exit:
            Environment.Exit(0);
            break;
        case ProjektFeladat.Command.Help:
            Console.WriteLine(Cmd.Help(Cmd.ConvertCmd(Splits[1] == null ? "" : Splits[1])));
            break;
        default:
            AnsiConsole.MarkupLine("[bold red] not implemented![/]");
            break;
    }
}
