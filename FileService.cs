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
        var json = File.ReadAllText(filePath);

        var data = JsonSerializer.Deserialize<Data>(json);
        AuthorService.LoadAuthors(data.Authors);
        BookService.LoadBooks(data.Books);
    }
}
