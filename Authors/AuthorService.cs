using BookManager.Books;

namespace BookManager.Authors;

public static class AuthorService
{
    private static List<Author> authors = new List<Author>();

    public static void LoadAuthors(List<Author> newAuthors)
    {
        authors = newAuthors;
    }

    public static List<Author> GetAuthors() => authors;

    public static void SelectOperation()
    {
        var isExit = false;
        do {
            Console.WriteLine("Mit szeretne csinálni");
            Console.WriteLine("1 - Új, 2 - Módosítás, 3 - Törlés, 4 - Listázás, 5 - Kilépés");

            if (!Enum.TryParse(Console.ReadLine(), out Operation selection))
            {
                Console.WriteLine("Hibás választás");
                continue;
            }
            
            switch (selection)
            {
                case Operation.Create:
                    Create();
                    break;
                case Operation.Update:
                    Update();
                    break;
                case Operation.Delete:
                    Delete();
                    break;
                case Operation.List:
                    List();
                    break;
                case Operation.Exit:
                    isExit = true;
                    break;
            }
        } while (!isExit);
    }

    private static void Create()
    {
        var isExit = false;
        do
        {
            var newAuthor = new Author();

            Console.WriteLine("Kérem a szerző nevét:");
            newAuthor.Name = Console.ReadLine();

            Console.WriteLine("Kérem a szerző születésének évét:");
            if (!int.TryParse(Console.ReadLine(), out var year))
            {
                Console.WriteLine("Hibás szám");
                continue;
            }

            if (year < 1500)
            {
                Console.WriteLine("Hibás év");
                continue;
            }

            newAuthor.BornYear = year;
            newAuthor.Id = authors.Any() ? authors.Max(author => author.Id) + 1 : 1;

            authors.Add(newAuthor);
            isExit = true;
        } while (!isExit);
    }

    private static void Update()
    {
        var isExit = false;
        do
        {
            Console.WriteLine("Melyik szerzőt szeretné módosítani (Id):");
            List();

            if (!int.TryParse(Console.ReadLine(), out var authorId))
            {
                Console.WriteLine("Hibás szám");
                continue;
            }
            
            var author = authors.SingleOrDefault(book => book.Id == authorId);
            if (author is null)
            {
                Console.WriteLine("Szerző nem található");
                continue;
            }

            Console.WriteLine($"Kérem a könyv új címét ({author.Name}):");
            author.Name = Console.ReadLine();

            Console.WriteLine("Kérem a szerző születésének évét:");
            if (!int.TryParse(Console.ReadLine(), out var year))
            {
                Console.WriteLine("Hibás év");
                continue;
            }

            if (year < 1500)
            {
                Console.WriteLine("Hibás év");
                continue;
            }

            author.BornYear = year;

            Console.WriteLine("Kérem az alábbi könyvek közül válasszon");
            BookService.List();
            Console.WriteLine("Adja meg a könyv Id-ját");
            if (!int.TryParse(Console.ReadLine(), out var bookId))
            {
                Console.WriteLine("hibás szám formátum!");
                continue;
            }

            var book = BookService.GetBooks().FirstOrDefault(book => book.Id == bookId);
            if (book is null)
            {
                Console.WriteLine("Könyv nem található!");
                continue;
            }

            if (author.Books.Any(book => book.Id == bookId))
            {
                Console.WriteLine("Ez a könyv már létezik ennél a szerzőnél!");
                continue;
            }

            author.Books.Add(book);

            isExit = true;
        } while (!isExit);
    }

    private static void Delete()
    {
        var isExit = false;
        do
        {
            Console.WriteLine("Melyik szerzőt szeretné törölni (Id):");
            List();

            if (!int.TryParse(Console.ReadLine(), out var authorId))
            {
                Console.WriteLine("Hibás szám");
                continue;
            }

            var author = authors.SingleOrDefault(author => author.Id == authorId);
            if (author is null)
            {
                Console.WriteLine("Könyv nem található");
                continue;
            }

            authors.Remove(author);
            isExit = true;
        } while (!isExit);
    }

    public static void List()
    {
        foreach (var author in authors)
        {
            Console.WriteLine($"{author.Id}\t{author.Name}\t{author.BornYear}");
        }
    }
}
