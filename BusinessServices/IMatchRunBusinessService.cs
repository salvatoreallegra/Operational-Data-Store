/**********************************************
 * Interface for the Match Run Business Service
 * The Match run business service is where the
 * business logic is defined when calling
 * the main get request of the application
 * **************************************/
using ODSApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ODSApi.BusinessServices
{
    public interface IMatchRunBusinessService
    {
         Task<ServiceResponse<List<MatchRunEntity>>> getByMatchSequence(int match_id, int PtrSequenceNumber);

    }
}
