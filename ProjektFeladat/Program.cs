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

Command cm = new Command();
List<Konyv> Books = new KonyvReader().LoadBooks(args[0]);

// main loop
while (true)
{
    string command = AnsiConsole.Prompt(new TextPrompt<string>("[bold]enter command: [/]"));

    String[] splits = command.Split(' ');
    Command cmd = new();
    Commands type = cmd.ConvertCmd(splits[0]);

    switch (type)
    {
        case Commands.Search:
            cm.Search(String.Join(" ", splits[1..]), Books);
            break;
        case Commands.Add: // TODO
            goto default;
        case Commands.Remove: // TODO
            goto default;
        case Commands.Save: // TODO
            goto default;
        case Commands.Exit:
            Environment.Exit(0);
            break;
        case Commands.Help:
            Console.WriteLine(cm.Help(cm.ConvertCmd(splits[1] == null ? "" : splits[1])));
            break;
        default:
            AnsiConsole.MarkupLine("[bold red] not implemented![/]");
            break;
    }
}
