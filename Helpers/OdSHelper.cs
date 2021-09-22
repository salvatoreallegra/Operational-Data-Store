using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Helpers
{    public class OdSHelper
    {
        public string CalculateModelUsed(float offerKdpi)
        {
            string modelUsed = "Too good to model";
            if (offerKdpi >=43 && offerKdpi <= 64)
            {
                modelUsed = "Kdpi-30";
                return modelUsed;
            } else if (offerKdpi >=65 && offerKdpi <= 100)
            {
                modelUsed = "Kdpi-50";
                return modelUsed;
            }
            return modelUsed;
        }
    }
}
