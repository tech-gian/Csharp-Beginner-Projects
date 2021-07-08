using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Collections
{
    class CsvReader
    {
        private string _csvFilePath;
        public CsvReader(string csvFilePath)
        {
            this._csvFilePath = csvFilePath;
        }

        public List<Country> ReadAllCountries()
        {
            List<Country> countries = new List<Country>();

            using (StreamReader sr = new StreamReader(_csvFilePath))
            {
                // read header line
                sr.ReadLine();

                string csvLine;
                while ((csvLine = sr.ReadLine()) != null)
                {
                    countries.Add(ReadCountryFromCsvFile(csvLine));
                }
            }

            return countries;
        }

        public Dictionary<string, Country> ReadAllCountriesDict()
        {
            var countries = new Dictionary<string, Country>();

            using (StreamReader sr = new StreamReader(_csvFilePath))
            {
                // read header line
                sr.ReadLine();

                string csvLine;
                while ((csvLine = sr.ReadLine()) != null)
                {
                    Country country = ReadCountryFromCsvFile(csvLine);
                    countries.Add(country.CountryCode, country);
                }
            }

            return countries;
        }

        public Dictionary<string, List<Country>> ReadAllCountriesNested()
        {
            var countries = new Dictionary<string, List<Country>>();

            using (StreamReader sr = new StreamReader(_csvFilePath))
            {
                // read header line
                sr.ReadLine();

                string csvLine;
                while ((csvLine = sr.ReadLine()) != null)
                {
                    Country country = ReadCountryFromCsvFile(csvLine);

                    if (countries.ContainsKey(country.Continent))
                    {
                        countries[country.Continent].Add(country);
                    }
                    else
                    {
                        List<Country> countriesInRegion = new List<Country>() { country };
                        countries.Add(country.Continent, countriesInRegion);
                    }
                }

                return countries;
            }
        }

        public Country ReadCountryFromCsvFile(string csvLine)
        {
            string[] parts = csvLine.Split(',');
            string name;
            string code;
            string region;
            string popText;

            switch (parts.Length)
            {
                case 4:
                    name = parts[0];
                    code = parts[1];
                    region = parts[2];
                    popText = parts[3];
                    break;
                case 5:
                    name = parts[0] + ", " + parts[1];
                    name = name.Replace("\"", null).Trim();
                    code = parts[2];
                    region = parts[3];
                    popText = parts[4];
                    break;
                default:
                    throw new Exception($"Can't parse country from csvLine: {csvLine}");
            }

            int.TryParse(popText, out int population);
            return new Country(name, code, region, population);
        }
    }
}
