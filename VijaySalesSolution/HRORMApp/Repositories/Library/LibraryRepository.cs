﻿using HRORMApp.DbContexts;
using HRORMApp.Entities.Library;
using Microsoft.EntityFrameworkCore;

namespace HRORMApp.Repositories.Library
{
    public class LibraryRepository
    {
        private LibraryContext _context;
        public LibraryRepository(LibraryContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            using (var context = new LibraryContext())
            {
                var author = new Author { Name = "Deepak Shikarpurkar" };
                author.Books = new List<Book>
                {
                    new Book { Title = "AT at workplace" },
                    new Book { Title = "UnEmployement reasons" },
                    new Book { Title = "Robotis at Tata Motors" }
                };
                context.Authors.Add(author);
                context.SaveChanges();
            }
        }
        public void ShowAll()
        {
            using (var context = new LibraryContext())
            {
                var authorsWithBooks = context.Authors.Include(a => a.Books).ToList();

                foreach (var author in authorsWithBooks)
                {
                    Console.WriteLine($"Author: {author.Name}");

                    foreach (var book in author.Books)
                    {
                        Console.WriteLine($" - Book: {book.Title}");
                    }
                }
            }
        }
    }
}
