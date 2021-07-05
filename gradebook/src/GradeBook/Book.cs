using System;
using System.Collections.Generic;

namespace GradeBook {
    
    class Book {
        // With or without the private keyword, is the same thing
        private List<double> grades;
        string name;
        
        // Constructor
        public Book(string name) {
            grades = new List<double>();
            this.name = name;
        }

        // Public Methods
        public void AddGrade(double grade) {
            grades.Add(grade);    
        }

        public void ShowStats() {
            var result = 0.0;
            var highGrade = double.MinValue;
            var lowGrade = double.MaxValue;
            foreach (double number in grades) {
                result += number;
                highGrade = Math.Max(number,highGrade);
                lowGrade = Math.Min(number, lowGrade);
            }
            result /= grades.Count;

            System.Console.WriteLine($"The lowest grade is {lowGrade}");
            System.Console.WriteLine($"The highest grade is {highGrade}");
            System.Console.WriteLine($"The average grade is {result:N1}");
        }
    }

}
