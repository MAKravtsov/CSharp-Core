using System.Collections.Generic;
using System.Linq;

namespace VKontakte.Infrastructure
{
    public class Policies
    {
        public const string AdministratorPolicy = "Administrator";
        public const string MangerPolicy = "Manger";

        public static IEnumerable<string> AllPolicies
        {
            get
            {
                var fields = typeof(Policies).GetFields();
                var result = fields.Select(y => (string)y.GetValue(null));
                return result;
            }
        }
    }
}
