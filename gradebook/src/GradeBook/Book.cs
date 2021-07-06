using System;
using System.Collections.Generic;

namespace GradeBook {
    
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class Book {
        // With or without the private keyword, is the same thing
        private List<double> grades;

        // Auto get-set property
        public string Name {
            // get and set can be private
            get;
            set;
        }

        // Can only be initialized or changed in the constructor
        readonly string category = "Science";
        // instead of readonly, there is const (can't be changed anywhere)
        


        // Constructor
        public Book(string name) {
            grades = new List<double>();
            this.Name = name;
        }

        // Public Methods
        public void AddGrade(char letter) {
            switch (letter) {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                default:
                    AddGrade(0);
                    break;

            }
        }

        public event GradeAddedDelegate GradeAdded;

        public void AddGrade(double grade) {
            if (grade <= 100 && grade >= 0) {
                grades.Add(grade);
                if (GradeAdded != null) {
                    GradeAdded(this, new EventArgs());
                }
                
            }
            else {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public Stats GetStats() {
            var result = new Stats();
            result.Average = 0.0;
            result.High = double.MinValue;
            result.Low = double.MaxValue;
            
            foreach (double grade in grades) {
                result.Average += grade;
                result.High = Math.Max(grade, result.High);
                result.Low = Math.Min(grade, result.Low);
            }
            result.Average /= grades.Count;

            switch (result.Average) {
                case var d when d >= 90.0:
                    result.Letter = 'A';
                    break;
                case var d when d >= 80.0:
                    result.Letter = 'B';
                    break;
                case var d when d >= 70.0:
                    result.Letter = 'C';
                    break;
                case var d when d >= 60.0:
                    result.Letter = 'D';
                    break;
                default:
                    result.Letter = 'F';
                    break;
            }

            return result;
        }
    }

}
