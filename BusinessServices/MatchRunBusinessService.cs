using ODSApi.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.BusinessServices
{

    
    public class MatchRunBusinessService
    {
        private readonly IMatchRunDBService _matchRunService;
        private readonly IMortalitySlopeDBService _mortalitySlopeService;
        private readonly ITimeToNextOfferDBService _timeToBetterService;
        private readonly ILogDBService _logDBService;

        public MatchRunBusinessService(IMatchRunDBService matchRunService, IMortalitySlopeDBService mortalitySlopeService, ITimeToNextOfferDBService timeToNextOfferService, ILogDBService logDBService)
        {
            _matchRunService = matchRunService ?? throw new ArgumentNullException(nameof(matchRunService));
            _mortalitySlopeService = mortalitySlopeService ?? throw new ArgumentNullException(nameof(mortalitySlopeService));
            _timeToBetterService = timeToNextOfferService ?? throw new ArgumentNullException(nameof(timeToNextOfferService));
            _logDBService = logDBService ?? throw new ArgumentNullException(nameof(logDBService));

        }

    }
}
