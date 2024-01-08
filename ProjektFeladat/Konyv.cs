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
        internal List<Konyv> LoadBooks(string path)
        {
            StreamReader file;

            // fájlbeolvasós try-catch
            try
            {
                file = new(path);
            }
            catch (Exception e)
            {
                throw new Exception("konyvek [file]", e);
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

            return Books;
        }
    }
    internal class KonyvWriter
    {
        internal void SaveBooks(string path, List<Konyv> Books)
        {
            // fájlbeolvasós try-catch
            try
            {
                File.WriteAllText(path, "");
            }
            catch (Exception e)
            {
                throw new Exception("konyvek [file]", e);
            }
            // YAML szerializáló létrehozása, és a beolvasott lista szerializálása
            var serializer = new SerializerBuilder().Build();
            var serializedBook = serializer.Serialize(Books);

            // fájlba írás
            File.WriteAllText(path, serializedBook);
        }
    }
    internal class KonyvManager
    {
        internal void AddBook(Konyv book)
        {
            // TODO - lados vagy macza
        }
        internal void RemoveBook(Konyv book)
        {
            // TODO - lados vagy macza
        }
    }

}
