using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using pruebatecnicapeaku.Client.Repositories;
using pruebatecnicapeaku.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace pruebatecnicapeaku.Client.Auth
{
    public class AuthenticationProviderJWT : AuthenticationStateProvider, ILoginService
    {
        public AuthenticationProviderJWT(IJSRuntime js, HttpClient httpClient,
            IRepository repository)
        {
            this.js = js;
            this.httpClient = httpClient;
            this.repository = repository;
        }

        private AuthenticationState Anonymus =>
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));



        public static readonly string TOKENKEY = "TOKENKEY";
        public static readonly string EXPIRATIONTOKENKEY = "EXPIRATIONTOKENKEY";
        private readonly IJSRuntime js;
        private readonly HttpClient httpClient;
        private readonly IRepository repository;

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await js.GetFromLocalStorage(TOKENKEY);

            if (string.IsNullOrEmpty(token))
            {
                return Anonymus;
            }

            var ExpirationTimeString = await js.GetFromLocalStorage(EXPIRATIONTOKENKEY);
            DateTime ExpirationTime;

            if (DateTime.TryParse(ExpirationTimeString, out ExpirationTime))
            {
                if (TokenExpired(ExpirationTime))
                {
                    await Clean();
                    return Anonymus;
                }

                if (ShallRenewToken(ExpirationTime))
                {
                    token = await RewToken(token);
                }

            }

            return BuilsAuthenticationState(token);
        }

        public async Task HandleRenovationToken()
        {
            var ExpiracionTimeString = await js.GetFromLocalStorage(EXPIRATIONTOKENKEY);
            DateTime ExpiracionTime;

            if (DateTime.TryParse(ExpiracionTimeString, out ExpiracionTime))
            {
                if (TokenExpired(ExpiracionTime))
                {
                    await Logout();
                }

                if (ShallRenewToken(ExpiracionTime))
                {
                    var token = await js.GetFromLocalStorage(TOKENKEY);
                    var newToken = await RewToken(token);
                    var authState = BuilsAuthenticationState(newToken);
                    NotifyAuthenticationStateChanged(Task.FromResult(authState));
                }
            }
        }

        private async Task<string> RewToken(string token)
        {
           
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var newTokenResponse = await repository.Get<UserToken>("api/account/RenewToken");
            var newToken = newTokenResponse.Response;
            await js.SetInLocalStorage(TOKENKEY, newToken.Token);
            await js.SetInLocalStorage(EXPIRATIONTOKENKEY, newToken.Expiration.ToString());
            return newToken.Token;
        }

        private bool ShallRenewToken(DateTime ExpirationTime)
        {
            return ExpirationTime.Subtract(DateTime.UtcNow) < TimeSpan.FromMinutes(5);
        }

        private bool TokenExpired(DateTime ExpiracionTime)
        {
            return ExpiracionTime <= DateTime.UtcNow;
        }

        public AuthenticationState BuilsAuthenticationState(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public async Task Login(UserToken userToken)
        {
            await js.SetInLocalStorage(TOKENKEY, userToken.Token);
            await js.SetInLocalStorage(EXPIRATIONTOKENKEY, userToken.Expiration.ToString());
            var authState = BuilsAuthenticationState(userToken.Token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await Clean();
            NotifyAuthenticationStateChanged(Task.FromResult(Anonymus));
        }

        private async Task Clean()
        {
            await js.RemoveItem(TOKENKEY);
            await js.RemoveItem(EXPIRATIONTOKENKEY);
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
