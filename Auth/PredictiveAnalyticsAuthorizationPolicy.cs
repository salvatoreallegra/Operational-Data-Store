using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public static class PredictiveAnalyticsAuthorizationPolicy
    {
        public const string Name = "PredictiveAnalytics";

        public static AuthorizationPolicy Policy =>
            new AuthorizationPolicyBuilder().RequireClaim("http://schemas.microsoft.com/identity/claims/scope", "api.predictiveanalytics").Build();
    }
}

