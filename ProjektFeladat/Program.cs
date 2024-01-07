using ProjektFeladat;
using Spectre.Console;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using YamlDotNet.Serialization;

Command cm = new Command();
List<Konyv> Books = new KonyvReader().LoadBooks(args[0]);
void Run(string command)
{
    Command cmd = new();
    Commands type = cmd.ConvertCmd(command);

    switch (type)
    {
        case Commands.None:
            throw new NotImplementedException("not implemented");
        case Commands.Search:
            break;
        case Commands.Add:
            break;
        case Commands.Remove:
            break;
    }
}

AnsiConsole.MarkupLine("[bold yellow]Welcome![/]");
AnsiConsole.Markup("[blue]Leave the prompt empty to list all books.[/]");
AnsiConsole.MarkupLine("[red]Press CTRL+C to exit[/]");
Console.WriteLine("type 'q [query]' to search");
Console.WriteLine("type 'a [book_title] [book_author] [book_genre] [book_url]' to add a book.");
Console.WriteLine("type 'r [query]' to delete a book.");

// main loop
while (true)
{
    string search = AnsiConsole.Prompt(new TextPrompt<string>("[bold]enter command: [/]").AllowEmpty());

    cm.Search(search, Books);
}
