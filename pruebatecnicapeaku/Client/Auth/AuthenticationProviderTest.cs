using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace pruebatecnicapeaku.Client.Auth
{
    public class AuthenticationProviderTest : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonymus = new ClaimsIdentity(new List<Claim>() {
                 new Claim("key1","test" ),
                 new Claim(ClaimTypes.Name,"usertest" ),
                 new Claim(ClaimTypes.Role,"viewer")
                },"prueba")
                ;
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymus)));
        }
    }
}
