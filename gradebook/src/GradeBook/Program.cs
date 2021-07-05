using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            // var goes only with initialization
            // var x = 5.6;
            // double y;
            // y = 4.8;

            // Initialization of arrays
            // 1st way) double[] numbers = new double[3];
            // 2nd way)
            double[] numbers = new[] {12.7, 4.5, 3.2}; // else use new type_name[]
            
            // var result = numbers[0] + numbers[1] + numbers[2];
            var result = 0.0;
            foreach (double number in numbers) {
                result += number;
            }
            

            // Using List
            List<double> grades = new List<double>(); // could be used var instead of double
            grades.Add(56.1);
            // foreach is used the same way
            
            
            System.Console.WriteLine($"The average grade is {(result / numbers.Length):N3}");
            // N3 means that is going to be displayed a number with 3 digits


            if (args.Length > 0) {
                Console.WriteLine($"Hello, {args[0]}!");
            }
            else {
                Console.WriteLine("Hello!");
            }
        }
    }
}
