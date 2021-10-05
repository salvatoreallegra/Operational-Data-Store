using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.BusinessServices
{
    public interface IMatchRunBusinessService
    {
        //Task<IEnumerable<MatchRunEntity>> GetMultipleAsync(string query);
        //Task<MatchRunEntity> GetAsync(string id);
        //Task AddAsync(MatchRunEntity item);
        Task<ServiceResponseEntity<List<MatchRunEntity>>> getByMatchSequence(int match_id, int PtrSequenceNumber);
        //Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
    }
}
