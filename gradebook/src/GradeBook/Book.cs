using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook {
    
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject {
        public string Name {
            get;
            set;
        }
        
        public NamedObject(string name) {
            Name = name;
        }
    }


    public interface IBook {
        void AddGrade(double grade);
        Stats GetStats();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }


    public abstract class Book: NamedObject, IBook {
        public Book(string name) : base(name) {
        }

        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade);
        public abstract Stats GetStats();
    }


    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name) {
        }

        public override event GradeAddedDelegate GradeAdded;
        public override void AddGrade(double grade) {
            using(var writer = File.AppendText($"{Name}.txt")) {
                writer.WriteLine(grade);
                if (GradeAdded != null) {
                    GradeAdded(this, new EventArgs());
                }
            }
        }
        public override Stats GetStats() {
            var result = new Stats();

            using (var reader = File.OpenText($"{Name}.txt")) {
                var line = reader.ReadLine();
                while (line != null) {
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }
            }

            return result;
        }
    }


    public class InMemoryBook: Book {
        // With or without the private keyword, is the same thing
        private List<double> grades;

        // Can only be initialized or changed in the constructor
        // readonly string category = "Science";
        // instead of readonly, there is const (can't be changed anywhere)
        

        // Constructor
        public InMemoryBook(string name): base(name) {
            grades = new List<double>();
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

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade) {
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

        public override Stats GetStats() {
            var result = new Stats();

            foreach (double grade in grades) {
                result.Add(grade);
            }

            return result;
        }
    }

}
