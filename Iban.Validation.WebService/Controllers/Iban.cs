using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iban.CountryData;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Iban.Validation.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Iban : ControllerBase
    {
        private readonly IbanValidation _ibanValidation = new IbanValidation(
            new CountyIbanLengthInFileProvider("../Iban/CountryData/CountryCodes.txt"));

        // GET api/iban/validate/ibanString
        [HttpGet("validate/{iban}")]
        public ActionResult<IbanValidationResult> ValidateIban(string iban)
        {
            return _ibanValidation.Validate(iban);
        }

        public class MultipleIbans
        {
            public string[] Ibans { set; get; }
        }

        [HttpPost("validate")]
        public ActionResult<IbanValidationResult[]> ValidateIbans([FromBody]MultipleIbans body)
        {
            return body.Ibans.Select(iban => _ibanValidation.Validate(iban)).ToArray();
        }
    }
}
