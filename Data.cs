using BookManager.Authors;
using BookManager.Books;

namespace BookManager;

public class Data
{
    public List<Book> Books { get; set; } = new List<Book>();
    public List<Author> Authors { get; set; } = new List<Author>();
}
