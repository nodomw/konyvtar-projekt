using System.Text;
using YamlDotNet.Serialization;

namespace ProjektFeladat
{
    internal class Konyv // Könyv osztály
    {
       /* public Konyv(string title, string author="", string genre="", int year=0, string url="")
        {
            this.title = title;
            this.author = author;       
            this.genre = genre;
            this.year = year;   
            this.url = url;
        }*/
        public string title { get; set; } // Cím
        public string author { get; set; } // Szerző

        public string genre { get; set; } // Műfaj

        public int year { get; set; } // Kiadás éve

        public string url { get; set; } // Elérhetési URL
    }
    internal class Konyvek
    {
        public List<Konyv> books { get; set; } // List<Konyv>-et nem engedte a deszerializálásnál, ezért kellett ez.
    }
    internal class KonyvReader
    {
        public KonyvReader(string path)
        {
            this.path = path;
        }
        string path { get; set; }
        internal List<Konyv> LoadBooks()
        {
            StreamReader File;

            // fájlbeolvasós try-catch
            try
            {
                File = new(path);
            }
            catch (Exception e)
            {
                throw new Exception("konyvek [file]", e);
            }

            // YAML deszeriálizáló létrehozása, és a beolvasott lista deszerializálása
            var Deserializer = new DeserializerBuilder().Build();
            var DeserializedBook = Deserializer.Deserialize<Konyvek>(File.ReadToEnd()); // kiolvasott adatok átírása 'Konyvek' osztályba

            // Összes könyv adatának átírása listába
            List<Konyv> Books = new();
            foreach (var Item in DeserializedBook.books)
            {
                Books.Add(Item);
            }

            return Books;
        }
    }
    internal class KonyvWriter
    {
        public KonyvWriter(string path) { this.path = path; }
        public string path { get; set; }
        internal void SaveBooks(List<Konyv> Books)
        {
            // fájl létrehozása - ne tudjon Exceptiont dobni nem létező fájl miatt
            try
            {
                File.WriteAllText(path, "");
            }
            catch (Exception e)
            {
                throw new Exception("konyvek [file]", e);
            }
            // YAML szerializáló létrehozása, és a beolvasott lista szerializálása
            var Serializer = new SerializerBuilder().Build();
            var SerializedBook = Serializer.Serialize(Books);

            // need to append 'books:' to the beginning of the file cuz 4 some reason it doesnt and shit gets fucked without it
            StringBuilder Output = new();
            Output.AppendLine("books:");
            Output.AppendLine(SerializedBook);

            //Console.WriteLine(Output);
            // fájlba írás
            File.WriteAllText(path, Output.ToString());
        }
    }
    internal class KonyvManager
    {
        internal void AddBook(Konyv Book, List<Konyv> Books)
        {

            // könyv hozzáadása a listához
            Books.Add(Book);

            // konzolra kiírás a sikeres könyv hozzáadásról
            Console.WriteLine($"A könyv hozzáadva: {Book.title} - {Book.author}");
        }
        internal void RemoveBook(Konyv Book, List<Konyv> Books)
        {
            if (Book != null)
            {
                // könyv eltávolítása a listából
                bool removed = Books.Remove(Book);

                if (removed)
                {
                    // konzolra kiírás a sikeres könyveltávolításról
                    Console.WriteLine($"A könyv eltávolítva: {Book.title} - {Book.author}");

                    // frissítjük a könyvtárat
                    // UpdateLibrary(Books, libraryPath);
                }
                else
                {
                    // konzolra kiírás, ha a könyv nem található a listában
                    Console.WriteLine($"A könyv nem található a listában: {Book.title} - {Book.author}");
                }
            }
            else
            {
                // hibaüzenet, ha a törlendő könyv értéke null
                Console.WriteLine("A törlendő könyv értéke null.");
            }
        }
    }
}