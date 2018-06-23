using Newtonsoft.Json.Linq;
using System.Linq;

namespace MundiPag.MessageTranslator.SharedKernel.Business.Translate
{
    public class TranslateJsonMetadataRio : ITranslateJsonMetadataRio
    {
        public object Handle(object incoming)
        {
            JObject root = (JObject)incoming;

            bool bodyCityIsCollection = root["corpo"]["cidade"].GetType() == typeof(JArray);

            string json = string.Empty;
            if (!bodyCityIsCollection)
            {
                JProperty encounters = ((JObject)root["corpo"]["cidade"]).Property("bairros");
                encounters.Value = encounters.Value["bairro"];

                json = "{\"cidade\": [" + root["corpo"]["cidade"].ToString() + "]}";
            }
            else
            {

                string cityJson = "{" + ((JObject)root["corpo"]).Property("cidade").ToString() + "}";
                JObject newRoot = JObject.Parse(cityJson);

                json = "{\"cidade\": [";
                JToken lastJTokenCity = newRoot.SelectTokens("cidade[*]").Last();
                foreach (var dataCity in newRoot.SelectTokens("cidade[*]"))
                {
                    json += "{";
                    json += "\"nome\": \"" + dataCity["nome"].ToString() + "\",";
                    json += "\"populacao\": " + dataCity["populacao"].ToString() + ",";

                    json += "\"bairros\": [";

                    bool neighborhoodIsCollection = dataCity["bairros"]["bairro"].GetType() == typeof(JArray);

                    if (neighborhoodIsCollection)
                    {
                        JToken lastJTokenDataHeighborhood = dataCity.SelectTokens("bairros.bairro[*]").Last();
                        foreach (var dataHeighborhood in dataCity.SelectTokens("bairros.bairro[*]"))
                        {
                            json += "{";
                            json += "\"nome\": \"" + dataHeighborhood["nome"].ToString() + "\",";
                            json += "\"populacao\": " + dataHeighborhood["populacao"].ToString();
                            json += "}";
                            if (!lastJTokenDataHeighborhood.Equals(dataHeighborhood)) json += ",";
                        }
                    }
                    else
                    {
                        json += "{";
                        json += "\"nome\": \"" + dataCity["bairros"]["bairro"]["nome"].ToString() + "\",";
                        json += "\"populacao\": " + dataCity["bairros"]["bairro"]["populacao"].ToString();
                        json += "}";
                    }
                    json += "]";

                    json += "}";
                    if (!lastJTokenCity.Equals(dataCity)) json += ",";
                }
                json += "]}";
            }

            return json;
        }
    }
}
