using System;
using Xunit;

namespace GradeBook.Tests
{
    public delegate string WriteLogDelegate(string logMessage);


    public class TypeTests
    {
        // private field
        int count = 0;

        [Fact]
        public void WriteLogDelegateCanPointToMethod() {
            WriteLogDelegate log = ReturnMessage;  // This happens, new WriteLogDelegate(ReturnMessage)

            log += ReturnMessage;
            log += IncrementCount;

            var result = log("Hello!");
            Assert.Equal(3, count);
        }

        string IncrementCount(string message) {
            count++;
            return message.ToLower();
        }

        string ReturnMessage(string message) {
            count++;
            return message;
        }


        [Fact]
        public void Test1() {
            var x = GetInt();
            SetInt(ref x);

            Assert.Equal(42, x);
        }

        private void SetInt(ref int x) {
            x = 42;
        }

        private int GetInt() {
            return 3;
        }


        [Fact]
        public void CSharpCanPassByRef()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName2(ref book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }

        private void GetBookSetName2(ref InMemoryBook book, string name) {
            // instead of ref we can use out
            book = new InMemoryBook(name);
            book.Name = name;
        }


        [Fact]
        public void CSharpIsPassByValue()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New Name");

            Assert.Equal("Book 1", book1.Name);
        }

        private void GetBookSetName(InMemoryBook book, string name) {
            book = new InMemoryBook(name);
            book.Name = name;
        }


        [Fact]
        public void CanSetFromReference()
        {
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }

        private void SetName(InMemoryBook book, string name) {
            book.Name = name;
        }


        [Fact]
        public void StringsBehaveLikeValueTypes() {
            string name = "Giannis";
            var upper = MakeUpperCase(name);

            Assert.Equal("Giannis", name);
            Assert.Equal("GIANNIS", upper);
        }

        private string MakeUpperCase(string parameter) {
            return parameter.ToUpper();
        }


        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book1, book2);
        }


        [Fact]
        public void TwoVarsCanReferenceSameObject() {
            var book1 = GetBook("Book 1");
            var book2 = book1;

            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }

        InMemoryBook GetBook (string name) {
            return new InMemoryBook(name);
        }
    }
}
