using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Client.Mvc.ViewModels
{
    public class ClaimViewer
    {

        public ClaimViewer(string name, IEnumerable<Claim> claims)
        {
            Name = name;
            Claims = claims.ToList();
            Token = "N/A";  
        }

        public ClaimViewer(string name, string tokenJson)
        {
            Token = tokenJson;
            Name = name;
            Claims = ((JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(tokenJson)).Claims?.ToList();
        }

        public string Name { get; private set; }
        public List<Claim> Claims { get; private set; }
        public string Token { get; private set; }
    }
}
