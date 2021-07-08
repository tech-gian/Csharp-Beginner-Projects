using System;
using System.Collections.Generic;
using System.Text;

namespace Collections
{
    class Country
    {
        public string CountryName { get; }
        public string CountryCode { get; }
        public string Continent { get; }
        public int Population { get; }

        public Country(string countryName, string countryCode, string continent, int population)
        {
            this.CountryName = countryName;
            this.CountryCode = countryCode;
            this.Continent = continent;
            this.Population = population;
        }
    }
}
