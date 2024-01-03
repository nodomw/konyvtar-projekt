// See https://aka.ms/new-console-template for more information
// usage: booksearcher.exe [file] [query]
using ProjektFeladat;
using Spectre.Console;
using System.Diagnostics;
using YamlDotNet.Serialization;
StreamReader file; // fájl arg

// fájlbeolvasós try-catch
try
{
    file = new(args[0]);
}
catch (Exception e)
{
    throw e;
}
// YAML deszeriálizáló létrehozása, és a beolvasott lista deszerializálása
var deserializer = new DeserializerBuilder().Build();
var deserializedBook = deserializer.Deserialize<Konyvek>(file.ReadToEnd()); // kiolvasott adatok átírása 'Konyvek' osztályba

// Összes könyv adatának átírása listába
List<Konyv> Books = new();
foreach (var item in deserializedBook.books)
{
    Books.Add(item);
}

AnsiConsole.MarkupLine("[bold yellow]Welcome![/]\n\n[red]Press CTRL+C to exit[/]");
AnsiConsole.Markup("[blue]Leave the prompt empty to list all books.[/]");
AnsiConsole.MarkupLine("[red]Press CTRL+C to exit[/]");

// main loop
while (true)
{
    // tábla GUI előkészítése
    var table = new Table();

    table.AddColumn("Title");
    table.AddColumn("Year");
    table.AddColumn("Author");
    table.AddColumn("Genre");
    table.AddColumn("URL");
    table.Border(TableBorder.Rounded);

    string search = AnsiConsole.Prompt(new TextPrompt<string>("[bold]enter search term: [/]").AllowEmpty()); // keresés

    for (int i = 0; i < Books.Count; i++)
    {
        if (Books[i].title.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0) // indexes keresés a 'search' változó alapján, nagybetűket ignorálva. a keresés csak a cím alapján működik.
        {
            var konyv = Books[i]; // Jelenlegi könyv változó

            // egy sor hozzáadása az i-edik könyvvel
            table.AddRow(konyv.title, konyv.year.ToString(), konyv.author, konyv.genre, $"[blue underline]{konyv.url}[/]");
        }
    }
    
    AnsiConsole.Write(table); // tábla kiírása
}
