using System.Linq;
using Xunit;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using MundiPag.MessageTranslator.Api.Exchange;
using MundiPag.MessageTranslator.SharedKernel.Aggregations;
using MundiPag.MessageTranslator.SharedKernel.Contracts;
using MundiPag.MessageTranslator.SharedKernel.Messaging;
using MundiPag.MessageTranslator.SharedKernel.Adapters;
using Newtonsoft.Json;
using System.Xml.Linq;
using MundiPag.MessageTranslator.SharedKernel.Translate;

namespace MundiPag.MessageTranslator.Business.UnitTest
{
    public class LogisticMessageAdapterTests
    {
        #region JsonString

        const string jsonAcre = "{\"cities\":[{\"name\":\"Rio Branco\",\"population\":576589,\"neighborhoods\":[{\"name\":\"Habitasa\",\"population\":7503}]}]}";

        #endregion

        #region XmlString
        const string xmlRio = @"<corpo>
                                <cidade>
                                <nome>Rio de Janeiro</nome>
                                <populacao>10345678</populacao>
                                <bairros>
                                <bairro>
                                <nome>Tijuca</nome>
                                <regiao>Zona Norte</regiao>
                                <populacao>135678</populacao>
                                </bairro>
                                <bairro>
                                <nome>Botafogo</nome>
                                <regiao>Zona Sul</regiao>
                                <populacao>105711</populacao>
                                </bairro>
                                </bairros>
                                </cidade>
                                </corpo>";

        const string xmlMinasGerais = @"<body>
                                        <region>
                                        <name>Triangulo Mineiro</name>
                                        <cities>
                                        <city>
                                        <name>Uberlandia</name>
                                        <population>700001</population>
                                        <neighborhoods>
                                        <neighborhood>
                                        <name>Santa Monica</name>
                                        <zone>Zona Leste</zone>
                                        <population>13012</population>
                                        </neighborhood>
                                        </neighborhoods>
                                        </city>
                                        </cities>
                                        </region>
                                        </body>";
        #endregion

        [Fact]
        public async void Adapt_Json_Send_ReturnLogistMessageRegionAcre()
        {
            JObject @object = JObject.Parse(jsonAcre);

            var requestMessage = FactoryReflectionGenericExchange.Create<RequestMessageAcre>(@object.ToObject<Dictionary<string, object>>());

            IMessageAdapter<IRequestMessageAcre, LogisticMessage> adapt = 
                new LogisticMessageAdapterAcre<IRequestMessageAcre, LogisticMessage>();

            var result = await adapt.Adapt(requestMessage);

            Assert.IsAssignableFrom<IList<LogisticMessage>>(result);
            Assert.NotNull(result.FirstOrDefault());
            Assert.NotEmpty(result);
        }

        [Fact]
        public async void Adapt_XML_Send_ReturnLogistMessageRegionRio()
        {
            TranslateJsonMetadataRio translate = new TranslateJsonMetadataRio();

            XElement xml = XElement.Parse(xmlRio);

            JObject root = JObject.Parse(JsonConvert.SerializeXNode(xml));

            string json = (string)translate.Handle(root);

            JObject @object = JObject.Parse(json);

            var requestMessage = FactoryReflectionGenericExchange.Create<RequestMessageRio>(@object.ToObject<Dictionary<string, object>>());

            IMessageAdapter<IRequestMessageRio, LogisticMessage> adapt =
                new LogisticMessageAdapterRio<IRequestMessageRio, LogisticMessage>();

            var result = await adapt.Adapt(requestMessage);

            Assert.IsAssignableFrom<IList<LogisticMessage>>(result);
            Assert.NotNull(result.FirstOrDefault());
            Assert.NotEmpty(result);
        }

        [Fact]
        public async void Adapt_XML_Send_ReturnLogistMessageRegionMinasGerais()
        {
            TranslateJsonMetadataMinasGerais translate = new TranslateJsonMetadataMinasGerais();

            XElement xml = XElement.Parse(xmlMinasGerais);

            JObject root = JObject.Parse(JsonConvert.SerializeXNode(xml));

            string json = (string)translate.Handle(root);

            JObject @object = JObject.Parse(json);

            var requestMessage = FactoryReflectionGenericExchange.Create<RequestMessageMinasGerais>(@object.ToObject<Dictionary<string, object>>());

            IMessageAdapter<IRequestMessageMinasGerais, LogisticMessage> adapt =
                new LogisticMessageAdapterMinasGerais<IRequestMessageMinasGerais, LogisticMessage>();

            var result = await adapt.Adapt(requestMessage);

            Assert.IsAssignableFrom<IList<LogisticMessage>>(result);
            Assert.NotNull(result.FirstOrDefault());
            Assert.NotEmpty(result);
        }
    }
}
