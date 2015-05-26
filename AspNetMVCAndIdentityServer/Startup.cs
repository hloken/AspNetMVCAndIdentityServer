using System;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using AspNetMVCAndIdentityServer.IdentityServer;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.Core.Logging;
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
                    //RequireSsl = false,
                    Factory = InMemoryFactory.Create(
                        Users.Get(),
                        Clients.Get(),
                        StandardScopes.All),
                    SigningCertificate = LoadCertificate()
                });
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = "https://localhost:44300/identity",
                ClientId = "mvc",
                RedirectUri = "https://localhost:44300/",
                ResponseType = "id_token",
                SignInAsAuthenticationType = "Cookies",
            });

            LogProvider.SetCurrentLogProvider(new TraceSourceLogProvider());
        }

        X509Certificate2 LoadCertificate()
        {
            var fileName = string.Format(@"{0}bin\IdentityServer\cert.pfx", AppDomain.CurrentDomain.BaseDirectory);

            var password = new SecureString();
            foreach (var c in "test")
                password.AppendChar(c);
            
            return new X509Certificate2(fileName, password);
        }
    }
}