using LibraryApplication.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApplication.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;
        public BookService(ILibraryDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        public Book Get(int id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public List<Book> Search(string searchTerm = "") =>
            _books.Find(book => book.Name.ToLower().Contains(searchTerm.ToLower()) || book.Author.ToLower().Contains(searchTerm.ToLower())).ToList();

        public Book Create(Book book)
        {
            book.Id = this.LastId() + 1;
            _books.InsertOne(book);
            return book;
        }

        public void Update(int id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(int id) =>
            _books.DeleteOne(book => book.Id == id);

        public int LastId() =>
            _books.Find(book => true).SortByDescending(book => book.Id).FirstOrDefault().Id;
    }
}
