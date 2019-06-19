using System.Collections.Generic;

namespace Iban.CountryData
{
    /// <summary>
    /// Defines getting the IBAN length based on country code.
    /// This interface is added just for case when multiple sources are available
    /// (http calls, file, hardcoded etc).
    /// </summary>
    public interface ICountryIbanLengthProvider
    {
        Dictionary<string, int> GetLengthsForCountries();
    }
}