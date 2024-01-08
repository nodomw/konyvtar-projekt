using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace ProjektFeladat
{
    internal class Konyv // Könyv osztály
    {
        public string Title { get; set; } // Cím
        public string Author { get; set; } // Szerző

        public string Genre { get; set; } // Műfaj

        public int Year { get; set; } // Kiadás éve
        
        public string Url { get; set; } // Elérhetési URL
    }
    internal class Konyvek
    {
        public List<Konyv> Books { get; set; } // List<Konyv>-et nem engedte a deszerializálásnál, ezért kellett ez.
    }
    internal class KonyvReader
    {
        internal List<Konyv> LoadBooks(string path)
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
            foreach (var Item in DeserializedBook.Books)
            {
                Books.Add(Item);
            }

            return Books;
        }
    }
    internal class KonyvWriter
    {
        internal void SaveBooks(string path, List<Konyv> Books)
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

            // fájlba írás
            File.WriteAllText(path, SerializedBook);
        }
    }
    internal class KonyvManager
    {
        internal void AddBook(Konyv Book, List<Konyv> Books)
        {
            if (Book != null)
            {
                Books.Add(Book);
                Console.WriteLine($"A könyv hozzáadva: {Book.Title} - {Book.Author}");
            }
            else
            {
                Console.WriteLine("A hozzáadandó könyv értéke null.");
            }
        }
        internal void RemoveBook(Konyv Book, List<Konyv> Books)
        {
            if (Book != null)
            {
                var removed = Books.Remove(Book);
                if (removed)
                {
                    Console.WriteLine($"A könyv eltávolítva: {Book.Title} - {Book.Author}");
                }
                else
                {
                    Console.WriteLine($"A könyv nem található a listában: {Book.Title} - {Book.Author}");
                }
            }
            else
            {
                Console.WriteLine("A törlendő könyv értéke null.");
            }
        }
    }

}
