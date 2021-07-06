using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new Book("My GradeBook");
            book.GradeAdded += OnGradeAdded;
            book.GradeAdded += OnGradeAdded;
            book.GradeAdded -= OnGradeAdded;
            book.GradeAdded += OnGradeAdded;


            System.Console.WriteLine("Enter a grade or q to exit:");
            var input = Console.ReadLine();
            while (input != "q") {
                try {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch(ArgumentException ex) {
                    System.Console.WriteLine(ex.Message);
                    // throw
                    // this will terminate the program
                }
                catch (FormatException ex) {
                    System.Console.WriteLine(ex.Message);
                }
                finally {
                    // either try block executes
                    // either catch block executes,
                    // this block executes anyway
                    System.Console.WriteLine("**");
                }

                System.Console.WriteLine("Enter a grade or q to exit:");
                input = Console.ReadLine();
            }
            
            var stats = book.GetStats();
            
            System.Console.WriteLine($"For the book named {book.Name}");
            System.Console.WriteLine($"The lowest grade is {stats.Low}");
            System.Console.WriteLine($"The highest grade is {stats.High}");
            System.Console.WriteLine($"The average grade is {stats.Average:N1}");
            System.Console.WriteLine($"The letter is {stats.Letter}");
        }

        static void OnGradeAdded(object sender, EventArgs e) {
            System.Console.WriteLine("A grade was added");
        }
    }
}
