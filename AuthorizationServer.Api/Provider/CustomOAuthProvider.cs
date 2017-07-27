using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace AuthorizationServer.Api
{
    /// <summary>
    /// 验证客户端和资源所有者用户凭据
    /// </summary>
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// ValidateClientAuthentication”将负责通过从请求中读取client_id值来验证资源服务器（受众）是否已经在我们的授权服务器中注册，注意请求只包含没有共享对称密钥的client_id。如果我们采取快乐的场景并且观众被注册，我们将上下文标记为有效的上下文，这意味着观众检查已经过去，代码流可以进行到验证资源所有者凭据（正在请求的用户）的下一步令牌）。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            //对称秘钥的64位编码
            string symmetricKeyAsBase64 = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                return Task.FromResult<object>(null);
            }

            var audience = AudiencesStore.FindAudience(context.ClientId);

            if (audience == null)
            {
                context.SetError("invalid_clientId", string.Format("Invalid client_id '{0}'", context.ClientId));
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// “GrantResourceOwnerCredentials” 给资源所有者颁发证书
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //Dummy check here, you need to do your DB checks against memebrship system http://bit.ly/SPAAuthCode
            if (context.UserName != context.Password)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect");
                //return;
                return Task.FromResult<object>(null);
            }

            var identity = new ClaimsIdentity("JWT");

            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Manager"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Supervisor"));
            //请注意，我们正在将这些声明的身份验证类型设置为“JWT”，我们将受众客户端ID作为“AuthenticationProperties”的属性传递，我们将在下一步中使用受众客户端ID。
            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {"audience", context.ClientId ??string.Empty},
                    {"username", context.UserName}
                });

            var ticket = new AuthenticationTicket(identity, props);
            //现在，当我们调用“context.Validated（ticket）”时，将生成JWT访问令牌，但是我们仍然需要在步骤1.3中实现我们讨论的类“CustomJwtFormat”。
            context.Validated(ticket);
            return Task.FromResult<object>(null);
        }
    }
}