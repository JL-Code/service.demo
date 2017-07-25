using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mecode.toolkit
{
    /// <summary>
    /// jwt令牌参数
    /// </summary>
    public class JwtTokenOptions
    {

        /// <summary>
        /// 接收该JWT的一方，是否使用是可选的
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// RSA密钥。
        /// </summary>
        public RsaSecurityKey Key { get; set; }
        /// <summary>
        /// 签名证书
        /// </summary>
        public SigningCredentials Credentials { get; set; }
        /// <summary>
        /// 该JWT的签发者，是否使用是可选的
        /// </summary>
        public string Issuer { get; set; }

    }
}
