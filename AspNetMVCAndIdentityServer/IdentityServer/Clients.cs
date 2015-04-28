﻿using System.Collections.Generic;
using Thinktecture.IdentityServer.Core.Models;

namespace AspNetMVCAndIdentityServer.IdentityServer
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client
                {
                    Enabled = true,
                    ClientName = "MVC Client",
                    ClientId = "mvc",
                    Flow = Flows.Implicit,

                    RedirectUris = new List<string>
                    {
                        "http://localhost:60362/"
                    }
                }
            };
        }
    }
}