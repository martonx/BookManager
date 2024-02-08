using BookManager.Authors;
using BookManager.Books;
using System.Text.Json;

namespace BookManager;

public static class FileService
{
    private static string filePath = "data.json";

    public static void Save()
    {
        var data = new Data
        {
            Authors = AuthorService.GetAuthors(),
            Books = BookService.GetBooks(),
        };

        var json = JsonSerializer.Serialize(data);
        File.WriteAllText(filePath, json);
    }

    public static void Load()
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File nem létezik, mentsél előbb!");
            return;
        }

        var json = File.ReadAllText(filePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            Console.WriteLine("Létezik, de üres a file!");
            return;
        }

        try
        {
            var data = JsonSerializer.Deserialize<Data>(json);
            AuthorService.LoadAuthors(data.Authors);
            BookService.LoadBooks(data.Books);
        }
        catch (Exception)
        {
            Console.WriteLine("Hibás az adat a file-ban!");
        }
    }
}
