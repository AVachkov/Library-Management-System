using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace Library_Management_System
{
    class Program
    {
        static void Main()
        {
            
        }
    }

    class Book
    {
        public string Isbn { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public int YearPublished { get; private set; }
        public double Price { get; private set; }
        // Genre, PageCount, or Publisher

        public Book(string isbn, string title, string author, int yearPublished, double price)
        {
            if (string.IsNullOrWhiteSpace(isbn) || !Regex.IsMatch(isbn, @"\d{13}") || !Regex.IsMatch(isbn, @"\d{10}"))
                throw new ArgumentException($"Incorrect ISBN format: '0000000000000' or '0000000000' (13 or 10 digits)");

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException($"Title cannot be empty.");
            
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException($"Author cannot be empty.");

            if (yearPublished < 1700 || yearPublished > DateTime.Now.Year)
                throw new ArgumentException($"Book cannot be older than 1000.");

            if (price < 0 || price > 100)
                throw new ArgumentException($"Price cannot be negative or more that 100$.");
            
            Isbn = isbn;
            Title = title;
            Author = author;
            YearPublished = yearPublished;
            Price = price;
        }
    }
}