using Newtonsoft.Json.Linq;
using System.Linq;

namespace MundiPag.MessageTranslator.SharedKernel.Business.Translate
{
    public class TranslateJsonMetadataMinasGerais : ITranslateJsonMetadataMinasGerais
    {
        public object Handle(object incoming)
        {
            JObject root = (JObject)incoming;

            bool bodyCityIsCollection = root["body"]["region"]["cities"]["city"].GetType() == typeof(JArray);

            string json = string.Empty;
            if (!bodyCityIsCollection)
            {
                json = "{\"cities\": [{";
                json += "\"name\": \"" + root["body"]["region"]["cities"]["city"]["name"].ToString() + "\",";
                json += "\"population\": " + root["body"]["region"]["cities"]["city"]["population"].ToString() + ",";

                bool neighborhoodIsCollection = root["body"]["region"]["cities"]["city"]["neighborhoods"]["neighborhood"].GetType() 
                    == typeof(JArray);

                if (!neighborhoodIsCollection)
                {
                    json += "\"neighborhoods\": [{";
                    json += "\"name\": \"" + root["body"]["region"]["cities"]["city"]["neighborhoods"]["neighborhood"]["name"].ToString() + "\",";
                    json += "\"population\": " + root["body"]["region"]["cities"]["city"]["neighborhoods"]["neighborhood"]["population"].ToString();
                    json += "}]";
                }
                else
                {
                    json += "\"neighborhoods\": [";
                    JToken lastJTokenDataHeighborhood = root["body"]["region"]["cities"]["city"].SelectTokens("neighborhoods.neighborhood[*]").Last();
                    foreach (var dataHeighborhood in root["body"]["region"]["cities"]["city"].SelectTokens("neighborhoods.neighborhood[*]"))
                    {
                        json += "{";
                        json += "\"name\": \"" + dataHeighborhood["name"].ToString() + "\",";
                        json += "\"population\": " + dataHeighborhood["population"].ToString();
                        json += "}";
                        if (!lastJTokenDataHeighborhood.Equals(dataHeighborhood)) json += ",";
                    }
                    json += "]";

                }

                json += "}]}";
            }
            else
            {
                json = "{\"cities\": [";
                
                JToken lastJTokenDataCity = root["body"]["region"].SelectTokens("cities.city[*]").Last();
                foreach (var dataCity in root["body"]["region"].SelectTokens("cities.city[*]"))
                {
                    json += "{";
                    json += "\"name\": \"" + dataCity["name"].ToString() + "\",";
                    json += "\"population\": " + dataCity["population"].ToString() + ",";

                    bool neighborhoodIsCollection = dataCity["neighborhoods"]["neighborhood"].GetType()
                    == typeof(JArray);

                    if (!neighborhoodIsCollection)
                    {
                        json += "\"neighborhoods\": [{";
                        json += "\"name\": \"" + dataCity["neighborhoods"]["neighborhood"]["name"].ToString() + "\",";
                        json += "\"population\": " + dataCity["neighborhoods"]["neighborhood"]["population"].ToString();
                        json += "}]}";
                    }
                    else
                    {
                        json += "\"neighborhoods\": [";
                        JToken lastJTokenDataHeighborhood = dataCity.SelectTokens("neighborhoods.neighborhood[*]").Last();
                        foreach (var dataHeighborhood in dataCity.SelectTokens("neighborhoods.neighborhood[*]"))
                        {
                            json += "{";
                            json += "\"name\": \"" + dataHeighborhood["name"].ToString() + "\",";
                            json += "\"population\": " + dataHeighborhood["population"].ToString();
                            json += "}";
                            if (!lastJTokenDataHeighborhood.Equals(dataHeighborhood)) json += ",";
                        }
                        json += "]}";

                    }

                    if (!lastJTokenDataCity.Equals(dataCity)) json += ",";
                }

                json += "]}";
            }

            return json;
        }
    }
}
