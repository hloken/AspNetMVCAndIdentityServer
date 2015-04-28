using AspNetMVCAndIdentityServer.IdentityServer;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.Core.Models;

namespace AspNetMVCAndIdentityServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/identity", idsrvApp =>
            {
                idsrvApp.UseIdentityServer(new IdentityServerOptions
                {
                    SiteName = "Embedded IdentityServer",
                    RequireSsl = false,
                    Factory = InMemoryFactory.Create(
                        Users.Get(),
                        Clients.Get(),
                        StandardScopes.All)
                    //,SigningCertificate = LoadCertificate()
                });
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = "http://localhost:60362/identity",
                ClientId = "mvc",
                RedirectUri = "http://localhost:60362/",
                ResponseType = "id_token",
                SignInAsAuthenticationType = "Cookies"
            });
        }



        //X509Certificate2 LoadCertificate()
        //{
        //    return new X509Certificate2(
        //        string.Format(@"{0}\bin\identityServer\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        //}
    }
}