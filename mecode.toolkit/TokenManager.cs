using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace mecode.toolkit
{
    /// <summary>
    /// 令牌管理
    /// </summary>
    public class TokenManager
    {

        /// <summary>
        /// 创建Jwt令牌
        /// </summary>
        /// <param name="expire">过期时间</param>
        /// <param name="audience">接收jwt者</param>
        /// <param name="credentials">签名证书</param>
        public static string CreateJwtToken(Identity identity, DateTime expire, string audience, SigningCredentials credentials)
        {
            var handler = new JwtSecurityTokenHandler();
            string jti = audience + identity.Name + expire.Millisecond;
            jti = MD5Util.GetMD5(jti); // Jwt 的一个参数，用来标识 Token
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, string.Empty), // 添加角色信息
                new Claim(ClaimTypes.NameIdentifier, identity.Id.ToString()), // 用户 Id 
                new Claim("jti",jti,ClaimValueTypes.String) // jti，用来标识 token
            };
            //一个基于声明的标识
            var cid = new ClaimsIdentity(new GenericIdentity(identity.Name, "TokenAuth"), claims);
            var token = handler.CreateEncodedJwt(new SecurityTokenDescriptor
            {//包含用于创建安全令牌的一些信息。
                Issuer = "TestIssuer", // 指定 Token 签发者，也就是这个签发服务器的名称
                Audience = audience, // 指定 Token 接收者
                SigningCredentials = credentials,//签名证书
                Subject = cid, //该JWT所面向的用户，是否使用是可选的
                Expires = expire //jwt 过期时间
            });
            return token;
        }

        public static string CreateJwtToken(ClaimsIdentity claimsIdentity, DateTime expire, string audience, SigningCredentials credentials)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateEncodedJwt(new SecurityTokenDescriptor
            {//包含用于创建安全令牌的一些信息。
                Issuer = "TestIssuer", // 指定 Token 签发者，也就是这个签发服务器的名称
                Audience = audience, // 指定 Token 接收者
                SigningCredentials = credentials,//签名证书
                Subject = claimsIdentity, //该JWT所面向的用户，是否使用是可选的
                Expires = expire //jwt 过期时间
            });
            return token;
        }


        public static void ValidToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
