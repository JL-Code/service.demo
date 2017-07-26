using mecode.toolkit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace service.tests
{
    [TestClass]
    public class TokenManagerTest
    {
        [TestCategory("Jwt_Token_Test")]
        [TestMethod]
        public void Generate_RSA_Credentials()
        {
            var path = "E:\\";
            RSAUtil.GenerateAndSaveKey(path);
        }

        [TestCategory("Jwt_Token_Test")]
        [TestMethod]
        public void Try_GetKeyParameters_Test()
        {
            var path = "E:\\";
            var flag = RSAUtil.TryGetKeyParameters(path, true, out RSAParameters keyParameters);
            Assert.AreEqual(flag, true);
        }
    }
}
