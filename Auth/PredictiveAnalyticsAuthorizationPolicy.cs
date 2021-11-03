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
        public static AuthorizationPolicy Policy => new AuthorizationPolicyBuilder().RequireClaimScopes("auth.predictiveanalytics").Build();
       
    }
}

