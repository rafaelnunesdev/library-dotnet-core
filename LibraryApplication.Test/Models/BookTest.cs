using LibraryApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LibraryApplication.Test.Models
{
    public class BookTest
    {
        [Fact]
        public void Should_Create_Book_Without_Loans()
        {
            Assert.Null(new Book().Loans);
        }

        [Fact]
        public void Should_Add_Book_Loan_Info()
        {
            var book = new Book();

            book.LoanTo("user");

            Assert.NotNull(book.Loans);
            Assert.Equal("user", book.Loans[0].User);
            Assert.Equal(0, book.Loans[0].Borrowed.CompareTo(DateTime.Today));
            Assert.Equal(0, book.Loans[0].Returned.CompareTo(DateTime.MinValue));
        }

        [Fact]
        public void Should_Add_Return_Date()
        {
            var book = new Book();

            book.LoanTo("user");
            book.Return();

            Assert.Equal(0, book.Loans[0].Returned.CompareTo(DateTime.Today));
        }

        [Fact]
        public void Should_Update_Book_Name_And_Author()
        {
            var book = new Book();
            book.Update("New book name", "New book author");

            Assert.Equal("New book name", book.Name);
            Assert.Equal("New book author", book.Author);
        }
    }
}
