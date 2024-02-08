namespace BookManager.Books;

public static class BookService
{
    private static List<Book> books = new List<Book>();

    public static void LoadBooks(List<Book> newBooks)
    {
        books = newBooks;
    }

    public static List<Book> GetBooks() => books;

    public static void List()
    {
        foreach (var book in books)
        {
            Console.WriteLine($"{book.Id}\t{book.Title}\t{book.PublishYear}\t{book.SoldNumber}");
        }
    }

    public static void SelectOperation()
    {
        var isExit = false;
        do
        {
            Console.WriteLine("Mit szeretne csinálni");
            Console.WriteLine("1 - Új, 2 - Módosítás, 3 - Törlés, 4 - Listázás, 5 - Visszalépés");

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
            var newBook = new Book();

            Console.WriteLine("Kérem a könyv címét:");
            newBook.Title = Console.ReadLine();
            newBook.Id = books.Any() ? books.Max(book => book.Id) + 1 : 1;

            if (!SetPublishYear(newBook))
                continue;

            if (!SetSoldNumber(newBook))
                continue;

            books.Add(newBook);
            isExit = true;
        } while (!isExit);
    }

    private static void Update()
    {
        var isExit = false;
        do
        {
            Console.WriteLine("Melyik könyvet szeretné módosítani (Id):");
            List();

            if (!int.TryParse(Console.ReadLine(), out var bookId))
            {
                Console.WriteLine("Hibás szám");
                continue;
            }

            var book = books.SingleOrDefault(book => book.Id == bookId);
            if (book is null)
            {
                Console.WriteLine("Könyv nem található");
                continue;
            }

            Console.WriteLine($"Kérem a könyv új címét ({book.Title}):");
            book.Title = Console.ReadLine();

            if (!SetPublishYear(book))
                continue;

            if (!SetPublishYear(book))
                continue;

            if (!SetSoldNumber(book))
                continue;

            isExit = true;
        } while (!isExit);
    }

    private static void Delete()
    {
        var isExit = false;
        do
        {
            Console.WriteLine("Melyik könyvet szeretné törölni (Id):");
            List();

            if (!int.TryParse(Console.ReadLine(), out var bookId))
            {
                Console.WriteLine("Hibás szám");
                continue;
            }

            var book = books.SingleOrDefault(book => book.Id == bookId);
            if (book is null)
            {
                Console.WriteLine("Könyv nem található");
                continue;
            }

            books.Remove(book);
            isExit = true;
        } while (!isExit);
    }

    private static bool SetPublishYear(Book book)
    {
        Console.WriteLine("Kérem a könyv kiadásának évét:");
        if (!int.TryParse(Console.ReadLine(), out var year))
        {
            Console.WriteLine("Hibás szám");
            return false;
        }

        if (year < 1500)
        {
            Console.WriteLine("Hibás év");
            return false;
        }

        book.PublishYear = year;
        return true;
    }

    private static bool SetSoldNumber(Book book)
    {
        Console.WriteLine("Kérem a könyv eladott darabszámát:");
        if (!int.TryParse(Console.ReadLine(), out var sold))
        {
            Console.WriteLine("Hibás szám");
            return false;
        }

        if (sold < 0)
        {
            Console.WriteLine("Hibás eladott darabszám");
            return false;
        }

        book.SoldNumber = sold;
        return true;
    }
}
