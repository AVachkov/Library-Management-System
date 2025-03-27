using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text.Unicode;

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
        public string Genre { get; private set; }
        public string Author { get; private set; }
        public int YearPublished { get; private set; }
        public int PageCount { get; private set; }
        public decimal Price { get; private set; }

        public Book(string isbn, string title, string genre, string author, int yearPublished, int pageCount, decimal price)
        {
            if (string.IsNullOrWhiteSpace(isbn) || !Regex.IsMatch(isbn, @"^\d{13}$|^\d{10}$"))
                throw new ArgumentException($"Incorrect ISBN format: '0000000000000' or '0000000000' (13 or 10 digits)");

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException($"Title cannot be empty.");

            if (string.IsNullOrWhiteSpace(genre))
                throw new ArgumentException($"Genre cannot be empty.");

            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException($"Author cannot be empty.");

            if (yearPublished < 1700 || yearPublished > DateTime.Now.Year)
                throw new ArgumentException($"Year must be between 1700 and the current year.");

            if (pageCount < 10 || pageCount > 1500)
                throw new ArgumentException($"Page count cannot be less that 10 or more than 1500.");

            if (price < 0 || price > 100)
                throw new ArgumentException($"Price cannot be negative or more that 100$.");

            Isbn = isbn;
            Title = title;
            Genre = genre;
            Author = author;
            YearPublished = yearPublished;
            PageCount = pageCount;
            Price = price;
        }
        public override string ToString() => $"ISBN: {Isbn}, Title: {Title}, Genre: {Genre}, Author: {Author}," +
            $" Year Published: {YearPublished}, Page Count: {PageCount}, Price: {Price:C}";
    }
    enum UserRights
    {
        ReadOnly,
        Editable,
        Admin
    }
    abstract class User
    {
        private static int _idCount = 0;
        public int UserId { get; private set; }
        public string FullName { get; private set; }
        public string Username { get; private set; }
        private string _passwordHash;
        public string Email { get; private set; }
        public Dictionary<Book, UserRights> BookPermissions { get; private set; }
        
        protected User(string fullName, string username, string password, string email)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException($"Full Name cannot be empty.");

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException($"Username cannot be empty.");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException($"Password cannot be empty.");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException($"Email cannot be empty.");

            UserId = ++_idCount;
            FullName = fullName;
            Username = username;
            _passwordHash = HashPassword(password);
            Email = email;
            BookPermissions = new Dictionary<Book, UserRights>();
        }

        public string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public bool VerifyPassword(string password)
        {
            return _passwordHash == HashPassword(password);
        }

        public override string ToString()
        {
            return $"User ID: {UserId}, Full Name: {FullName}, Username: {Username}, Email: {Email}";
        }
    }
}