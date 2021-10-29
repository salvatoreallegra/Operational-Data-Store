/********************************************************
*Auth Policy configuration provided by unos auth package
********************************************************/

using Microsoft.AspNetCore.Authorization;

namespace Auth
{
    public static class PredictiveAnalyticsAuthorizationPolicy
    {
        public const string Name = "PredictiveAnalytics";

        public static AuthorizationPolicy Policy =>
            new AuthorizationPolicyBuilder().RequireClaim("http://schemas.microsoft.com/identity/claims/scope", "auth.predictiveanalytics").Build();
    }
}

