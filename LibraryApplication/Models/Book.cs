using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApplication.Models
{
    public class Book
    {
        [BsonId]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public List<Loan> Loans { get; set; }

        public void LoanTo(string user)
        {
            if (this.Loans == null)
            {
                this.Loans = new List<Loan>();
            }
            this.Loans.Add(new Loan(user));
        }

        public void Return()
        {
            this.Loans.Last().Return();
        }

        public Book Update(string name, string author)
        {
            this.Name = name;
            this.Author = author;

            return this;
        }
    }
}
