using AuthorizationServer.Api.Entities;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace AuthorizationServer.Api
{
    public static class AudiencesStore
    {
        public static ConcurrentDictionary<string, Audience> AudiencesList = new ConcurrentDictionary<string, Audience>();

        static AudiencesStore()
        {
            AudiencesList.TryAdd("099153c2625149bc8ecb3e85e03f0022",
                                new Audience
                                {
                                    ClientId = "099153c2625149bc8ecb3e85e03f0022",
                                    Base64Secret = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw",
                                    Name = "ResourceServer.Api 1"
                                });
        }

        public static Audience AddAudience(string name)
        {
            //生成32个字符的随机字符串作为受众的标识符（客户端ID）。
            //使用所述“RNGCryptoServiceProvider|RandomNumberGenerator”类的256位随机密钥然后用Base64的URL编码它，该键将授权服务器和资源服务器之间共享只。
            //将新生成的观众添加到内存“AudiencesList”中。
            //“FindAudience”方法负责根据客户端ID查找受众。
            //该类的构造函数包含用于演示目的的固定受众。
            var clientId = Guid.NewGuid().ToString("N");

            var key = new byte[32];
            RandomNumberGenerator.Create().GetBytes(key);
            var base64Secret = TextEncodings.Base64Url.Encode(key);

            Audience newAudience = new Audience { ClientId = clientId, Base64Secret = base64Secret, Name = name };
            AudiencesList.TryAdd(clientId, newAudience);
            return newAudience;
        }

        public static Audience FindAudience(string clientId)
        {
            Audience audience = null;
            if (AudiencesList.TryGetValue(clientId, out audience))
            {
                return audience;
            }
            return null;
        }
    }
}