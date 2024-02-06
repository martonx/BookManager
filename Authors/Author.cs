using BookManager.Books;

namespace BookManager.Authors;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BornYear { get; set; }
    public List<Book> Books { get; set; }
}
