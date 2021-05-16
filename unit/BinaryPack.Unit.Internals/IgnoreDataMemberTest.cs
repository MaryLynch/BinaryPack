using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryPack.Unit
{
    public class AddressFormat
    {
        private CultureInfo? _cultureInfo;
        private string _cultureInfoName;
        public int Id { get; set; }

        public string Name { get; set; }

        [IgnoreDataMember]
        public CultureInfo CultureInfo
        {
            get { return _cultureInfo ??= CultureInfo.GetCultureInfo(CultureInfoName); } 
            set
            {
                CultureInfoName = value.Name;
                _cultureInfo = value; 
            }
        }

        public string CultureInfoName
        {
            get => _cultureInfoName;
            set => _cultureInfoName = value;
        }
    }

    [TestClass]
    public class IgnoreDataMemberTest
    {
        [TestMethod]
        public void IgnoreDataMember_IsSupported()
        {
            var target = new AddressFormat()
            {
                Id = 5,
                Name = "Anything",
                CultureInfo = CultureInfo.InvariantCulture
            };

            var result = BinaryConverter.Serialize(target);

            Assert.IsNotNull(result);

            var deserialized = BinaryConverter.Deserialize<AddressFormat>(result);

            Assert.AreEqual(target.CultureInfo, deserialized.CultureInfo);
        }
    }

}
