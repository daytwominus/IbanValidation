using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Iban.CountryData;

namespace Iban
{
    /// <summary>
    /// IbanValidation of IBAN string. The algorithm is based on
    /// https://en.wikipedia.org/wiki/International_Bank_Account_Number#Algorithms
    /// </summary>
    public class IbanValidation
    {
        private readonly Dictionary<string, int> _lengthForCountry;

        public IbanValidation(ICountryIbanLengthProvider countryIbanLengthProvider)
        {
            _lengthForCountry = countryIbanLengthProvider.GetLengthsForCountries();
        }

        public IbanValidationResult Validate(string ibanString)
        {
            ibanString = ibanString.ToUpper();

            if (string.IsNullOrEmpty(ibanString) || ibanString.Length < 2)
                return new IbanValidationResult(ibanString, "IBAN string is too short");

            var code = ibanString.Substring(0, 2);
            if(!_lengthForCountry.TryGetValue(code, out var length))
                return new IbanValidationResult(ibanString, $"No country code found: {code}");

            if(length != ibanString.Length)
                return new IbanValidationResult(ibanString, $"IBAN string has incorrect length");

            var ibanValueString = string.Concat(
                $"{ibanString.Substring(4)}{ibanString.Substring(0, 4)}"
                .ToCharArray()
                .Select(c => c >= 'A' && c <= 'Z' ? (c - 55).ToString() : c.ToString()));

            return BigInteger.Parse(ibanValueString) % 97 == 1 ?
                new IbanValidationResult(ibanString) { Success = true } :
                new IbanValidationResult(ibanString, "Invalid 97 check remainder");
        }
    }
}
