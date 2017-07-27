using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security;
using System.Web.Http;

[assembly: OwinStartup(typeof(ResourceServer.Api.Startup))]

namespace ResourceServer.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            ConfigureOAuth(app);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(config);

        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var issuer = "http://jwtauthzsrv.azurewebsites.net";
            var audience = "099153c2625149bc8ecb3e85e03f0022";
            var secret = TextEncodings.Base64Url.Decode("IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw");

            // Api controllers with an [Authorize] attribute will be validated with JWT
            // API控制器与[Authorize]属性来验证JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audience },//允许的受众
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                    }
                });

        }
    }
}
