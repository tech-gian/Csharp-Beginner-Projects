using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SomeUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //InsertSamurai();
            //InsertMultipleSamurais();
            SimpleSamuraiQuery();
        }

        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Giannis" };
            var samuraiSammy = new Samurai { Name = "Sampson" };
            using (var context = new SamuraiContext())
            {
                context.Samurais.AddRange(samurai, samuraiSammy);
                context.SaveChanges();
            }
        }

        private static void InsertSamurai()
        {
            var samurai = new Samurai
            {
                Name = "Giannis"
            };
            using (var context=new SamuraiContext())
            {
                context.Samurais.Add(samurai);
                context.SaveChanges();
            }

        }

        private static void SimpleSamuraiQuery()
        {
            using (var context = new SamuraiContext())
            {
                var samurais = context.Samurais.ToList();
                var query = context.Samurais;
                foreach (var samurai in query)
                {
                    Console.WriteLine(samurai.Name);
                }
            }
        }
    }
}
