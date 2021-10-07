using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Entities
{
    public class ServiceResponseEntity<T>
    {
        public T Data { get; set; }
        public string message { get; set; }
        public ERRORS errors { get; set; }

    }
    public enum ERRORS
    {
        NoPassThroughRecord,
        NoMortalitySlopeRecord,
        NoTimeToNextOfferRecord,
        DataValidationError,
        Ok
        
    }
}
