using System;
using System.Collections.Generic;

namespace Library.BL
{
    class UserInputRetriever
    {
        static bool isReadyToQuit = false;

        public static void DisplayStartMenu()
        {
            do
            {
                Console.WriteLine("\n\n ** MENU GŁÓWNE ** ");
                Console.WriteLine("------------------------\n");
                Console.WriteLine("Wybierz jedną z opcji:\n");
                Console.WriteLine("1: Dodaj książkę do katalogu");
                Console.WriteLine("2: Usuń książkę z katalogu");
                Console.WriteLine("3: Wyszukaj książkę po autorze");
                Console.WriteLine("4: Wyszukaj książkę po tytule");
                Console.WriteLine("5: Wyszukaj książkę po numerze ISBN");
                Console.WriteLine("6: Wyszukaj książki, które nie zostały wypożyczone przez ostatnie n tygodni");
                Console.WriteLine("7: Wypożycz książkę");
                Console.WriteLine("8: Wyszukaj użytkowników, posiadających aktualnie wypożyczone książki");
                Console.WriteLine("q: Wyjście z programu\n\n");
                string userOption = Console.ReadLine();
                RedirectUser(userOption);
            } while (!isReadyToQuit);
        }

        public static void RedirectUser(string userOption)
        {
            string retrievedISBN;
            string retrievedTitle;
            List<Author> retrievedAuthors;
            List<KeyValuePair<string, Book>> listOfBooks;

            switch (userOption)
            {
                case "1":
                    retrievedISBN = getISBN();
                    retrievedTitle = getTitle();
                    retrievedAuthors = getAuthors();
                    CatalogService.getInstance().AddBookToCatalog(retrievedISBN, new Book(retrievedTitle, retrievedAuthors));
                    break;

                case "2":
                    retrievedISBN = getISBN();
                    CatalogService.getInstance().RemoveBookFromCatalog(retrievedISBN);
                    break;

                case "3":
                    string retrievedAuthorFullName = getOneAuthor();
                    listOfBooks = CatalogService.getInstance().FindBookByAuthor(new Author(retrievedAuthorFullName));
                    Console.WriteLine($"\nWyniki wyszukiwania:\n");
                    foreach (KeyValuePair<string, Book> book in listOfBooks)
                    {
                        Console.WriteLine($"\n{book.Value}");
                        Console.WriteLine($"Numer ISBN: {book.Key}\n");
                    }
                    break;

                case "4":
                    retrievedTitle = getTitle().ToLower();
                    listOfBooks = CatalogService.getInstance().FindBookByTitle(retrievedTitle);
                    Console.WriteLine($"\nWyniki wyszukiwania:\n");
                    foreach (KeyValuePair<string, Book> book in listOfBooks)
                    {
                        Console.WriteLine($"\n{book.Value}");
                        Console.WriteLine($"Numer ISBN: {book.Key}\n");
                    }
                    break;

                case "5":
                    retrievedISBN = getISBN();
                    var FoundBook = CatalogService.getInstance().FindBookByISBN(retrievedISBN);
                    Console.WriteLine($"\nWyniki wyszukiwania:\n");
                    Console.WriteLine(FoundBook);
                    Console.WriteLine($"Numer ISBN: {retrievedISBN}\n");
                    break;

                case "6":
                    Console.WriteLine("\nPodaj liczbę tygodni:");
                    int numberOfWeeksWithoutBorrowing = int.Parse(Console.ReadLine());
                    listOfBooks = CatalogService.getInstance().FindBooksUnborrowedByNWeeks(numberOfWeeksWithoutBorrowing);
                    Console.WriteLine($"\nKsiążki, które nie zostały wypożyczone przez {numberOfWeeksWithoutBorrowing} tyg.: \n");
                    foreach (KeyValuePair<string, Book> book in listOfBooks)
                    {
                        Console.WriteLine($"{book.Value}, ostatnia data wypożyczenia to:{book.Value.LastBorrowDate}");
                        Console.WriteLine($"Numer ISBN: {book.Key}\n");
                    }
                    break;

                case "7":
                    retrievedISBN = getISBN();
                    Console.WriteLine("Podaj swoje imie i nazwisko:\n");
                    string customerFullName = Console.ReadLine();
                    string[] customerData=  splitText(customerFullName);
                    CatalogService.getInstance().BorrowBook(retrievedISBN, new Customer(customerData[0], customerData[1]));
                    break;

                case "8":
                    Console.WriteLine("\n** Osoby posiadające aktualnie wypożyczone książki to: \n");
                    var listOfCustomersWithBorrowedBooks = CatalogService.getInstance().FindCustomersWithBorrowedBooks();
                    foreach (Customer customer in listOfCustomersWithBorrowedBooks)
                    {
                        Console.WriteLine(customer.ToString());
                    }
                    break;

                case "q":
                    Console.WriteLine("\n******************************************");
                    Console.WriteLine("Dziękujemy za skorzystanie z biblioteki");
                    isReadyToQuit = true;
                    return;

                default:
                    Console.WriteLine("\nNie ma takij opcji. Aby wybrać jedną z opcji wpisz cyfrę 1-8 lub 'q' jeżli chcesz wyjść. ");
                    break;
            }
        }

        private static string[] splitText(string customerFullName)
        {
            return customerFullName.Split(" ");
        }

        private static string getISBN()
        {
            Console.WriteLine(" **** Podaj 13-cyfrowy kod ISBN książki:");
            string ISBN = Console.ReadLine();
            return ISBN;
        }

        private static string getTitle()
        {
            Console.WriteLine(" **** Podaj tytuł książki:");
            string title = Console.ReadLine();
            return title;
        }

        private static string getOneAuthor()
        {
            Console.WriteLine(" **** Podaj jednego z autorów książki:");
            string author = Console.ReadLine();
            return author;
        }
        
        private static List<Author> getAuthors()
        {
            bool hasOneAuthor = true;
            List<Author> authors = new List<Author>(); ;
            do
            {
                Console.WriteLine(" **** Podaj imię i nazwisko autora książki:");
                authors.Add(new Author(Console.ReadLine()));
                Console.WriteLine(" **** Czy chcesz podać innych autorów? y/n");
                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "y":
                        hasOneAuthor = false;
                        break;
                    case "n":
                        hasOneAuthor = true;
                        break;
                    default:
                        Console.WriteLine(" !!!! Niepoprawny znak, prosze o wpisanie 'y' lub 'n'");
                        break;
                }
            } while (!hasOneAuthor);
            return authors;
        }

    }
}
