using LibraryApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LibraryApplication.Test.Models
{
    public class LoanTest
    {
        [Fact]
        public void Should_Create_Loan_With_User_And_Date()
        {
            var loan = new Loan("user");

            Assert.Equal("user", loan.User);
            Assert.Equal(0, loan.Borrowed.CompareTo(DateTime.Today));
        }

        [Fact]
        public void Should_Fill_Return_Date_When_Returning()
        {
            var loan = new Loan("user");
            loan.Return();

            Assert.Equal(0, loan.Returned.CompareTo(DateTime.Today));
        }
    }
}
