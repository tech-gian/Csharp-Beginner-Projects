using System;
using System.Collections.Generic;
using System.Linq;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\zapan\Desktop\General\Programs\C#\Collections\Collections\Pop by Largest Final.csv";
            CsvReader reader = new CsvReader(filePath);

            // List
            List<Country> countries = reader.ReadAllCountries();
            Country lilliput = new Country("Lilliput", "LIL", "Somewhere", 2_000_000);
            int lilliputIndex = countries.FindIndex(x => x.Population < 2_000_000);
            countries.Insert(lilliputIndex, lilliput);
            countries.RemoveAt(lilliputIndex);

            for (int i=0; i<countries.Count; ++i)
            {
                Console.WriteLine($"{(countries[i].Population)}: {countries[i].CountryName}");
            }

            Console.WriteLine($"{countries.Count} countries");


            // Dictionary
            // Country norway = new Country("Norway", "NOR", "Europe", 5_282_223);
            // Country finland = new Country("Finland", "FIN", "Europe", 5_511_303);

            // Dictionary<string, Country> countriesDict = new Dictionary<string, Country>();
            // countriesDict.Add(norway.CountryCode, norway);
            // countriesDict.Add(finland.CountryCode, finland);

            // foreach (var country in countriesDict)
            // {
            //     Console.WriteLine(country.Value.CountryName);
            // }


            Dictionary<string, Country> countriesDict = reader.ReadAllCountriesDict();

            Console.WriteLine("Which country code do you want to look up?");
            string userInput = Console.ReadLine();

            bool gotCountry = countriesDict.TryGetValue(userInput, out Country countryD);
            if (!gotCountry)
            {
                Console.WriteLine($"Sorry, there is no country with code, {userInput}");
            }
            else
            {
                Console.WriteLine($"{countryD.CountryName} has population {countryD.Population}");
            }

            // Tricks with for loop
            Console.WriteLine("Enter no. of countries to display> ");
            bool inputIsInt = int.TryParse(Console.ReadLine(), out int user);
            if (!inputIsInt || user <= 0)
            {
                Console.WriteLine("You must type in a +ve integer. Exiting");
                return;
            }

            int maxToDisplay = user;
            for (int i=0; i<countries.Count; ++i)
            {
                if (i > 0 && (i % maxToDisplay == 0))
                {
                    Console.WriteLine("Hit return to continue, anything else to quit> ");
                    if (Console.ReadLine() != "")
                    {
                        break;
                    }
                }

                Country country = countries[i];
                Console.WriteLine($"{i+1}: {country.Population}: {country.CountryName}");
            }


            // Nested Dictionary
            Dictionary<string, List<Country>> countriesNested = reader.ReadAllCountriesNested();
            foreach (string region in countriesNested.Keys)
            {
                Console.WriteLine(region);
            }

            Console.WriteLine("Which of the above regions do you want?");
            string chosenRegion = Console.ReadLine();

            if (countriesNested.ContainsKey(chosenRegion))
            {
                foreach (Country country in countriesNested[chosenRegion].Take(10))
                {
                    Console.WriteLine($"{country.Population}: {country.CountryName}");
                }
            }
            else
            {
                Console.WriteLine("That is not a valid region");
            }
        }
    }
}
