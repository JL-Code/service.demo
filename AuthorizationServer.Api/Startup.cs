using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

[assembly: OwinStartup(typeof(AuthorizationServer.Api.Startup))]

namespace AuthorizationServer.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            // Web API routes
            config.MapHttpAttributeRoutes();

            //配置OAuth授权认证
            ConfigureOAuth(app);

            //运行跨域
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(config);
        }

        /// <summary>
        /// 配置OAuth认证授权
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureOAuth(IAppBuilder app)
        {

            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth2/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),//30分钟过期
                //指定了如何在名为“CustomOAuthProvider”的自定义类中验证客户端和资源所有者用户凭据的实现。
                Provider = new CustomOAuthProvider(),
                //我们已经指定了如何使用JWT格式生成访问令牌的实现，这个名为“CustomJwtFormat”的自定义类将负责使用DPAPI生成JWT而不是默认访问令牌，请注意，它们都使用承载方案。
                AccessTokenFormat = new CustomJwtFormat("http://douhua.oicp.net")
            };
            //生成JWT的路径将是：“http://douhua.oicp.net/oauth2/token”。
            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
        }
    }
}
