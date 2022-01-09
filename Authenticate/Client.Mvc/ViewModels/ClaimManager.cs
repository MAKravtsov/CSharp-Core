using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Client.Mvc.ViewModels
{
    public class ClaimManager
    {
        public List<ClaimViewer> Items { get; set; }

        public ClaimManager(HttpContext context, ClaimsPrincipal user)
        {
            Items = new List<ClaimViewer>();
            var claims = user.Claims.ToList();

            var idJsonToken = context.GetTokenAsync("id_token").GetAwaiter().GetResult();
            var accessJsonToken = context.GetTokenAsync("access_token").GetAwaiter().GetResult();

            AddTokenInfo("Identity Token", idJsonToken);
            AddTokenInfo("Access Token", accessJsonToken);
            AddTokenInfo("User Claims", claims);
        }

        public string AccessToken
        {
            get { return Items.SingleOrDefault(y => y.Name == "Access Token")?.Token; }
        }

        private void AddTokenInfo(string nameToken, string idTokenJson)
        {
            Items.Add(new ClaimViewer(nameToken, idTokenJson));
        }

        private void AddTokenInfo(string nameToken, IEnumerable<Claim> claims)
        {
            Items.Add(new ClaimViewer(nameToken, claims));
        }
    }
}
