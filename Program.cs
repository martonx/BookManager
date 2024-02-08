// See https://aka.ms/new-console-template for more information
using BookManager;
using BookManager.Authors;
using BookManager.Books;

var isExit = false;
do
{
    Console.WriteLine("Mit szeretne csinálni");
    Console.WriteLine("1 - Könyv, 2 - Szerző, 3 - Mentés, 4 - Betöltés, 5 - Kilépés");

    if (!Enum.TryParse(Console.ReadLine(), out Manage selection))
    {
        Console.WriteLine("Hibás választás");
        continue;
    }

    switch (selection)
    {
        case Manage.Book:
            BookService.SelectOperation();
            break;
        case Manage.Author:
            AuthorService.SelectOperation();
            break;
        case Manage.Save:
            FileService.Save();
            break;
        case Manage.Load:
            FileService.Load();
            break;
        case Manage.Exit:
            isExit = true;
            break;
        default:
            Console.WriteLine("Hibás választás");
            break;
    }
} while (!isExit);