/********************************************************
*Auth Policy configuration provided by unos auth package
********************************************************/

using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Auth
{
    public static class PredictiveAnalyticsAuthorizationPolicy
    {
        public const string Name = "PredictiveAnalytics";

        private readonly static IEnumerable<string> scopeClaimTypes = new List<string>
        {

           { "http://schemas.microsoft.com/identity/claims/scope" },
           { "scp" }

        };

        public static AuthorizationPolicy Policy =>
           new AuthorizationPolicyBuilder().RequireAssertion((context) =>
               context.User.Claims
                   .Where(x => scopeClaimTypes.Contains(x.Type))
                   .SelectMany(x => x.Value.Split(" "))
                   .Any(x => x.Equals("auth.predictiveanalytics", StringComparison.OrdinalIgnoreCase)))
           .Build();

        /*public static AuthorizationPolicy Policy =>
            new AuthorizationPolicyBuilder().RequireClaim("http://schemas.microsoft.com/identity/claims/scope", "auth.predictiveanalytics").Build();*/
    }
}

