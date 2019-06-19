using Iban;
using Iban.CountryData;
using NUnit.Framework;

namespace Tests
{
    public class IbanTests
    {
        private readonly IbanValidation _ibanValidation = new IbanValidation(new CountryIbanLengthInMemoryProvider());

        [SetUp]
        public void Setup()
        {
        }

        [TestCase("GB82WEST12345698765432")]
        [TestCase("AL35202111090000000001234567")]
        [TestCase("AD1400080001001234567890")]
        [TestCase("AT483200000012345864")]
        [TestCase("LB92000700000000123123456123")]
        [TestCase("MT31MALT01100000000000000000123")]
        [TestCase("MC5810096180790123456789085")]
        [TestCase("SC52BAHL01031234567890123456USD")]
        [TestCase("BY86AKBB10100000002966000000")]
        [TestCase("VG21PACG0000000123456789")]
        public void TestValidIbans(string iban)
        {
            Assert.IsTrue(_ibanValidation.Validate(iban).Success);
        }

        [TestCase("")]
        [TestCase("GB")]
        [TestCase("GB1")]
        [TestCase("GB82WEST1234569876543")]
        public void TestInvalidIbans(string iban)
        {
            Assert.IsFalse(_ibanValidation.Validate(iban).Success);
        }
    }
}