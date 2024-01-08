using ProjektFeladat;
using Spectre.Console;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using YamlDotNet.Serialization;

AnsiConsole.MarkupLine("[bold yellow]Welcome![/]");
AnsiConsole.MarkupLine("[red]type 'exit' to exit.[/]");
AnsiConsole.MarkupLine("[bold]type 'help' for all the commands[/]");

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
            if (Splits.Length >= 2 )
            {
                Console.WriteLine(Cmd.Help(Cmd.ConvertCmd(Splits[1])));
            }
            break;
        default:
            AnsiConsole.MarkupLine("[bold red] not implemented![/]");
            break;
    }
}
