using System;
using System.Collections.Generic;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            // Exampe for Random
            var numbers = MyLinq.Random().Where(n => n > 0.5).Take(10);
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }

            var movies = new List<Movie>
            {
                new Movie { Title = "The Dark Knight", Rating = 8.9f, Year = 2008},
                new Movie { Title = "The Knight's Speech", Rating = 8.0f, Year = 2010},
                new Movie { Title = "Casablanca", Rating = 8.5f, Year = 1942},
                new Movie { Title = "Star Was V", Rating = 8.7f, Year = 1980}
            };

            var query = movies.Filter(m => m.Year > 2000)
                              .OrderByDescending(m => m.Rating);
            //var query = from movie in movies
            //            where movie.Year > 2000
            //            orderby movie.Rating descending
            //            select movie;

            var enumerator = query.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Title);
            }
        }
    }
}
