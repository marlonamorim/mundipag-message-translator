using Newtonsoft.Json;

namespace MundiPag.MessageTranslator.SharedKernel.Aggregations
{
    public class Neighborhood
    {
        [JsonProperty("nome", Order = 1)]
        public string Name { get; set; }

        [JsonProperty("habitantes", Order = 1)]
        public int Population { get; set; }
    }
}
