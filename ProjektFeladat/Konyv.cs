using ProjektFeladat;
using System;
using System.Collections.Generic;
using System.IO;
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
        internal void AddBook(List<Konyv> Books, string libraryPath)
        {
            Konyv newBook = new Konyv();

            // Felhasználótól bekérjük a könyv adatait
            Console.WriteLine("Könyv hozzáadása:");
            Console.Write("Cím: ");
            newBook.title = Console.ReadLine();

            Console.Write("Szerző: ");
            newBook.author = Console.ReadLine();

            Console.Write("Műfaj: ");
            newBook.genre = Console.ReadLine();

            Console.Write("Kiadás év: ");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                newBook.year = year;
            }
            else
            {
                Console.WriteLine("Érvénytelen év formátum. Az év 0-ra lesz állítva.");
                newBook.year = 0;
            }

            Console.Write("URL: ");
            newBook.url = Console.ReadLine();

            // könyv hozzáadása a listához
            Books.Add(newBook);

            // konzolra kiírás a sikeres könyv hozzáadásról
            Console.WriteLine($"A könyv hozzáadva: {newBook.title} - {newBook.author}");
        }

        private static void UpdateLibrary(List<Konyv> Books, string libraryPath)
        {
            // könyvtár frissítése a szerializált listával
            var Serializer = new SerializerBuilder().Build();
            var SerializedBook = Serializer.Serialize(new Konyvek { books = Books });

            // Fájlba írás
            File.WriteAllText(libraryPath, SerializedBook);
        }
    }
    internal void RemoveBook(Konyv Book, List<Konyv> Books, string libraryPath)
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

    private void UpdateLibrary(List<Konyv> Books, string libraryPath)
    {
        // könyvtár frissítése a szerializált listával
        var Serializer = new SerializerBuilder().Build();
        var SerializedBook = Serializer.Serialize(new Konyvek { books = Books });

        // Fájlba írás
        File.WriteAllText(libraryPath, SerializedBook);
    }
}




// egy előző "működő" ideiglenes vezió -TÖRÖLD KI!-
// internal class KonyvManager
//{
    //internal void AddBook(Konyv Book, List<Konyv> Books)
    //{
        //if (Book != null)
        //{
            // könyv hozzáadása a listához
            //Books.Add(Book);
            // konzolra kiírás a sikeres könyv hozzáadásról
            //Console.WriteLine($"A könyv hozzáadva: {Book.title} - {Book.author}");
        //}
        //else
        //{
            // hibaüzenet, ha a hozzáadandó könyv értéke null
            //Console.WriteLine("A hozzáadandó könyv értéke null.");
        //}
    //}
    //internal void RemoveBook(Konyv Book, List<Konyv> Books)
    //{
        //if (Book != null)
        //{
            // könyv eltávolítása a listából
            //var removed = Books.Remove(Book);
            //if (removed)
            //{
                // konzolra kiírás a sikeres könyveltávolításról
                //Console.WriteLine($"A könyv eltávolítva: {Book.title} - {Book.author}");
            //}
            //else
            //{
                // konzolra kiírás, ha a könyv nem található a listában
                //Console.WriteLine($"A könyv nem található a listában: {Book.title} - {Book.author}");
            //}
        //}
        //else
        //{
            // hibaüzenet, ha a törlendő könyv értéke null
            //Console.WriteLine("A törlendő könyv értéke null.");
        //}
    //}
//}