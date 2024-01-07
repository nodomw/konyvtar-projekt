﻿using ProjektFeladat;
using Spectre.Console;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using YamlDotNet.Serialization;

Command cm = new Command();
List<Konyv> Books = new KonyvReader().LoadBooks(args[0]);

void Run(string command)
{
    string[] splits = command.ToLower().Split(' ');
    Command cmd = new();
    Commands type = cmd.ConvertCmd(splits[0]);

    switch (type)
    {
        case Commands.None:
            AnsiConsole.MarkupLine("[bold red] not implemented![/]");
            break;
        case Commands.Search:
            cm.Search(splits[1], Books); 
            Console.WriteLine("")
            break;
        case Commands.Add:
            break;
        case Commands.Remove:
            break;
        case Commands.Exit:
            Environment.Exit(0);
            break;
    }
}

AnsiConsole.MarkupLine("[bold yellow]Welcome![/]");
AnsiConsole.MarkupLine("[red]Type 'exit' to exit.[/]");
Console.WriteLine("");
AnsiConsole.MarkupLine("[bold white]COMMANDS:[/]");
Console.WriteLine("type 'q [query]' to search");
Console.WriteLine("type 'a [book_title] [book_author] [book_genre] [book_url]' to add a book.");
Console.WriteLine("type 'r [query]' to delete a book.");

// main loop
while (true)
{
    string command = AnsiConsole.Prompt(new TextPrompt<string>("[bold]enter command: [/]"));

    Run(command);
}
