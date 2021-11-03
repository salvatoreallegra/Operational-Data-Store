using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
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
        Ok

    }
}
