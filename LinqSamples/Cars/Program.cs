using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarDb>());
            InsertData();
            QueryData();


            //var query =
            //    from car in cars
            //    group car by car.Manufacturer into carGroup
            //    select new
            //    {
            //        Name = carGroup.Key,
            //        Max = carGroup.Max(c => c.Combined),
            //        Min = carGroup.Min(c => c.Combined),
            //        Avg = carGroup.Average(c => c.Combined)
            //    } into result
            //    orderby result.Max descending
            //    select result;

            //var query2 =
            //    cars.GroupBy(c => c.Manufacturer)
            //        .Select(g =>
            //        {
            //            var results = g.Aggregate(new CarStatistics(),
            //                                (acc, c) => acc.Accumulate(c),
            //                                acc => acc.Compute());
            //            return new
            //            {
            //                Name = g.Key,
            //                Avg = results.Average,
            //                Min = results.Min,
            //                Max = results.Max
            //            };
            //        })
            //        .OrderByDescending(r => r.Max);

            //foreach (var result in query) 
            //{
            //    Console.WriteLine($"{result.Name}");
            //    Console.WriteLine($"\t Max: {result.Max}");
            //    Console.WriteLine($"\t Min: {result.Min}");
            //    Console.WriteLine($"\t Avg: {result.Avg}");
            //}

            //var query =
            //    from car in cars
            //    join manufacturer in manufacturers
            //        on new { car.Manufacturer, car.Year }
            //        equals 
            //        new { Manufacturer = manufacturer.Name, manufacturer.Year }
            //    orderby car.Combined descending, car.Name ascending
            //    select new
            //    {
            //        manufacturer.Headquarters,
            //        car.Name,
            //        car.Combined
            //    };

            ////var result = cars.SelectMany(c => c.Name)
            ////                 .OrderBy(c => c);

            ////foreach (var character in result)
            ////{
            ////    Console.WriteLine(character);
            ////}

            //var query2 =
            //    cars.Join(manufacturers,
            //                c => new { c.Manufacturer, c.Year },
            //                m => new { Manufacturer = m.Name, m.Year },
            //                (c, m) => new
            //                {
            //                    m.Headquarters,
            //                    c.Name,
            //                    c.Combined
            //                })
            //        .OrderByDescending(c => c.Combined)
            //        .ThenBy(c => c.Name);

            //foreach (var car in query.Take(10))
            //{
            //    Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            //}
        }

        private static void QueryData()
        {
            var db = new CarDb();

            //var query = from car in db.Cars
            //            orderby car.Combined descending, car.Name ascending
            //            select car;

            //var query =
            //    db.Cars.Where(c => c.Manufacturer == "BMW")
            //           .OrderByDescending(c => c.Combined)
            //           .ThenBy(c => c.Name)
            //           .Take(10)
            //           .ToList();

            //var query =
            //    db.Cars.GroupBy(c => c.Manufacturer)
            //           .Select(g => new
            //           {
            //               Name = g.Key,
            //               Cars = g.OrderByDescending(c => c.Combined).Take(2)
            //           });

            var query =
                from car in db.Cars
                group car by car.Manufacturer into manufacturer
                select new
                {
                    Name = manufacturer.Key,
                    Cars = (from car in manufacturer
                           orderby car.Combined descending
                           select car).Take(2)
                };

            foreach (var group in query)
            {
                Console.WriteLine(group.Name);
                foreach (var car in group.Cars)
                {
                    Console.WriteLine($"\t{car.Name}: {car.Combined}");
                }
            }

            
        }

        private static void InsertData()
        {
            var cars = ProcessCars("fuel.csv");
            var db = new CarDb();

            if (!db.Cars.Any())
            {
                foreach (var car in cars)
                {
                    db.Cars.Add(car);
                }
                db.SaveChanges();
            }
        }

        private static void QueryXml()
        {
            var ns = (XNamespace)"http://pluralsight.com/cars/2016";
            var ex = (XNamespace)"http://pluralsight.com/cars/2016/ex";
            var document = XDocument.Load("fuel.xml");

            var query =
                from element in document.Element(ns + "Cars")?.Elements(ex + "Car") ?? Enumerable.Empty<XElement>()
                where element.Attribute("Manufacturer")?.Value == "BMW"
                select element.Attribute("Name").Value;

            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
        }

        private static void CreateXml()
        {
            var records = ProcessCars("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            var ns = (XNamespace)"http://pluralsight.com/cars/2016";
            var ex = (XNamespace)"http://pluralsight.com/cars/2016/ex";
            var document = new XDocument();
            var cars = new XElement(ns + "Cars",

                from record in records
                select new XElement(ex + "Car",
                                new XAttribute("Name", record.Name),
                                new XAttribute("Combined", record.Combined),
                                new XAttribute("Manufacturer", record.Manufacturer)));

            cars.Add(new XAttribute(XNamespace.Xmlns + "ex", ex));


            //foreach (var record in records)
            //{
            //    var name = new XAttribute("Name", record.Name);
            //    var combined = new XAttribute("Combined", record.Combined);
            //    var manufacturer = new XAttribute("Manufacturer", record.Manufacturer); 
            //    var car = new XElement("Car", name, combined, manufacturer);

            //    cars.Add(car);
            //}

            document.Add(cars);
            document.Save("fuel.xml");
        }

        private static List<Car> ProcessCars(string path)
        {
            var query =

                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(l => l.Length > 1)
                    .ToCar();

            return query.ToList();
        }

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Where(l => l.Length > 1)
                    .Select(l =>
                    {
                        var columns = l.Split(',');
                        return new Manufacturer
                        {
                            Name = columns[0],
                            Headquarters = columns[1],
                            Year = int.Parse(columns[2])
                        };
                    });
            return query.ToList();
        }
    }

    public class CarStatistics
    {
        public int Max { get; set; }
        public int Min { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
        public double Average { get; set; }

        public CarStatistics()
        {
            Max = Int32.MinValue;
            Min = Int32.MaxValue;
        }

        public CarStatistics Accumulate(Car car)
        {
            Count++;
            Total += car.Combined;
            Max = Math.Max(Max, car.Combined);
            Min = Math.Min(Min, car.Combined);
            return this;
        }

        public CarStatistics Compute()
        {
            Average = Total / Count;
            return this;
        }
    }

    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');

                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }
        }
    }
}
