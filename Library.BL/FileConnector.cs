using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Library.BL
{
    class FileConnector
    {
        public static Dictionary<string, Book> LoadCatalogFromFile() 
        {
            string fileName = @"C:\MB\12_C#\1_MyProjects\LibraryConsoleApp\Library.BL\BookCatalog.json";
            string jsonString = File.ReadAllText(fileName);
            Dictionary<string, Book> bookCatalog = JsonSerializer.Deserialize<Dictionary<string, Book>>(jsonString);
            return bookCatalog;
        }

        public static bool SaveCatalogToFile(Dictionary<string, Book> bookCatalog)
        {
            LoadCatalogFromFile();
            string fileName = @"C:\MB\12_C#\1_MyProjects\LibraryConsoleApp\Library.BL\BookCatalog.json";
            string jsonString = JsonSerializer.Serialize(bookCatalog);
            File.WriteAllText(fileName, jsonString);
            return true;
        }
    }
}
