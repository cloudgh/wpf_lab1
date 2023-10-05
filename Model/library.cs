using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lab1.Model
{
    public class User
    {
        public User(int id, string name, string lastname, string? email)
        {
            Id = id;
            Name = name;
            Lastname = lastname;
            Email = email;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string? Email { get; set; }

        public string FullName => $"{Name} {Lastname}";
    }
    public class Book
    {
        public Book(int id, string author, string title, DateTime releaseDate, int count)
        {
            Id = id;
            Author = author;
            Title = title;
            ReleaseDate = releaseDate;
            Count = count;
        }

        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Count { get; set; }

        public string FullBook => $"{Title} .Books left: {Count}";
    }
    public class UserBook
    {
        public UserBook(User user, Book book, int quantity)
        {
            User = user;
            Book = book;
            Quantity = quantity;
        }

        public User User { get; }
        public Book Book { get; }
        public int Quantity { get; set; }
    }

}