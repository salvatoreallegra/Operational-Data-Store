/**********************************************
 * Generic wrapper class that allows the piggy-
 * backing of a custom error enum type along with
 * the data that will be returned with the response.
 * *******************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Entities
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public ERRORS errors { get; set; }

    }
    public enum ERRORS
    {
        NoPassThroughRecord,
        NoMortalitySlopeRecord,
        NoTimeToNextOfferRecord,
        DataValidationError,
        MissingWaitListMortalityData,
        MissingTimeToNext30OrTimeToNext50Data,  
        Ok,
        Duplicates
        
    }
}
