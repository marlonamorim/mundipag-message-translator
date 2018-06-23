using MundiPag.MessageTranslator.SharedKernel.Business.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MundiPag.MessageTranslator.SharedKernel.Business.Aggregations
{
    public class LogisticMessage : ILogisticMessage
    {
        public LogisticMessage(string city, int cityPopulation, IList<Neighborhood> neighborhoods)
        {
            City = city;
            CityPopulation = cityPopulation;
            _neighborhoods = neighborhoods;
        }

        private readonly IList<Neighborhood> _neighborhoods;

        [JsonProperty("cidade", Order = 1)]
        public string City { get; protected set; }

        [JsonProperty("habitantes", Order = 2)]
        public int CityPopulation { get; protected set; }

        [JsonProperty("bairros", Order = 3)]
        public IReadOnlyCollection<Neighborhood> Neighborhoods { get { return _neighborhoods.ToArray(); } }
    }
}
