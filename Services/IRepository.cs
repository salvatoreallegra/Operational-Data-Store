﻿using ODSApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi.Services
{
    public interface IRepository
    {
        List<PredictiveModel> getAllModels();
        PredictiveModel GetPredictiveModelById(int Id);

        List<PredictiveModel> GetPredictiveModelsByCenterId(int centerId);
    }
}