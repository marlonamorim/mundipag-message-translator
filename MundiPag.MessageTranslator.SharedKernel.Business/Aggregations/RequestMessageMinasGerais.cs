using MundiPag.MessageTranslator.SharedKernel.Business.Compositions.MinasGerais;
using MundiPag.MessageTranslator.SharedKernel.Business.Contracts;

namespace MundiPag.MessageTranslator.SharedKernel.Business.Aggregations
{
    public class RequestMessageMinasGerais : IRequestMessageMinasGerais
    {
        public City[] Cities { get; set; }
    }
}
