namespace MundiPag.MessageTranslator.Api.Controllers {

    using Microsoft.AspNetCore.Mvc;
    using MundiPag.MessageTranslator.Api.Exchange;
    using MundiPag.MessageTranslator.SharedKernel.Aggregations;
    using MundiPag.MessageTranslator.SharedKernel.Contracts;
    using MundiPag.MessageTranslator.SharedKernel.Messaging;
    using MundiPag.MessageTranslator.SharedKernel.Translate;
    using MundiPag.MessageTranslator.Api.Authorization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    [BasicAuthorize()]
    [Route("api/[controller]")]
    public class TranslatorController : Controller {
        
        private readonly IMessageAdapter<IRequestMessageAcre, LogisticMessage> _messageAdapterAcre;
        private readonly IMessageAdapter<IRequestMessageRio, LogisticMessage> _messageAdapterRio;
        private readonly IMessageAdapter<IRequestMessageMinasGerais, LogisticMessage> _messageAdapterMinasGerais;
        private readonly ITranslateJsonMetadataRio _translateJsonMetadataRio;
        private readonly ITranslateJsonMetadataMinasGerais _translateJsonMetadataMinasGerais;

        public TranslatorController([FromServices] IMessageAdapter<IRequestMessageAcre, LogisticMessage> messageAdapterAcre,
                                    [FromServices] IMessageAdapter<IRequestMessageRio, LogisticMessage> messageAdapterRio,
                                    [FromServices] IMessageAdapter<IRequestMessageMinasGerais, LogisticMessage> messageAdapterMinasGerais,
                                    [FromServices] ITranslateJsonMetadataRio translateJsonMetadataRio,
                                    [FromServices] ITranslateJsonMetadataMinasGerais translateJsonMetadataMinasGerais)
        {
            _messageAdapterAcre = messageAdapterAcre;
            _messageAdapterRio = messageAdapterRio;
            _messageAdapterMinasGerais = messageAdapterMinasGerais;
            _translateJsonMetadataRio = translateJsonMetadataRio;
            _translateJsonMetadataMinasGerais = translateJsonMetadataMinasGerais;
        }

        [HttpPost]
        [Route("get-message/minasgerais/send-xml")]
        public async Task<IActionResult> TranslatorMinasGerais([FromBody]XElement xml)
        {
            JObject root = JObject.Parse(JsonConvert.SerializeXNode(xml));
            
            string json = (string)_translateJsonMetadataMinasGerais.Handle(root);

            JObject model = JObject.Parse(json);

            var adaptedMessage = await _messageAdapterMinasGerais.Adapt(FactoryReflectionGenericExchange.Create<RequestMessageMinasGerais>
                (model.ToObject<Dictionary<string, object>>()));

            return Ok(new { result = adaptedMessage });
        }

        [HttpPost]
        [Route("get-message/rio/send-xml")]
        public async Task<IActionResult> TranslatorRio([FromBody]XElement xml)
        {
            JObject root = JObject.Parse(JsonConvert.SerializeXNode(xml));

            string json = (string)_translateJsonMetadataRio.Handle(root);

            JObject model = JObject.Parse(json);

            var adaptedMessage = await _messageAdapterRio.Adapt(FactoryReflectionGenericExchange.Create<RequestMessageRio>
                (model.ToObject<Dictionary<string, object>>()));

            return Ok(new { result = adaptedMessage });
        }

        [HttpPost]
        [Route("get-message/send-json")]
        public async Task<IActionResult> Post([FromBody]JObject model)
        {
            var adaptedMessage = await _messageAdapterAcre.Adapt(FactoryReflectionGenericExchange.Create<RequestMessageAcre>
                (model.ToObject<Dictionary<string, object>>()));

            return Ok(new { result = adaptedMessage });
        }
    }
}
