using System.Collections.Generic;

namespace Iban.CountryData
{
    /// <summary>
    /// reading IBAN lengths from file. Data is taken from nordea website:
    /// https://www.nordea.com/en/our-services/cashmanagement/iban-validator-and-information/iban-countries/
    /// </summary>
    public class CountyIbanLengthInFileProvider: ICountryIbanLengthProvider
    {
        private string _file;

        public CountyIbanLengthInFileProvider(string file)
        {
            _file = file;
        }

        public Dictionary<string, int> GetLengthsForCountries()
        {
            string line;
            var lengthForCountry = new Dictionary<string, int>();
            var file = new System.IO.StreamReader(_file);
            while ((line = file.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line.Trim()))
                    continue;
                var splitted = line.Split(' ', '\t');
                lengthForCountry[splitted[1].ToUpper()] = int.Parse(splitted[0]);
            }

            return lengthForCountry;
        }
    }
}
