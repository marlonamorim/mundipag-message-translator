﻿using MundiPag.MessageTranslator.SharedKernel.Business.Aggregations;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MundiPag.MessageTranslator.SharedKernel.Business.Contracts
{
    public interface ILogisticMessage
    {
        [JsonProperty("cidade", Order = 1)]
        string City { get; }

        [JsonProperty("habitantes", Order = 2)]
        int CityPopulation { get; }

        [JsonProperty("bairros", Order = 3)]
        IReadOnlyCollection<Neighborhood> Neighborhoods { get; }
    }
}
