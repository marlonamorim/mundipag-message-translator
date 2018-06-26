using MundiPag.MessageTranslator.SharedKernel.Aggregations;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MundiPag.MessageTranslator.SharedKernel.Contracts
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
