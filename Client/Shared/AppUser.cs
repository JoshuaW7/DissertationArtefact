using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DissertationArtefact.Shared;

namespace DissertationArtefact.Client.Shared
{
    public class AppUser
    {
        public static string GetClaimValue(string ClaimType, IEnumerable<Claim> Claims)
        {
            foreach (var claim in Claims)
            {
                if (claim.Type == ClaimType)
                {
                    return claim.Value;
                }
            }
            return null;
        }


    }
}