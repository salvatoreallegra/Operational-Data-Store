using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Entities
{
    public class ServiceResponseEntity<T>
    {
        public T Data { get; set; }
        public int ResponseCode { get; set; }

    }
}
