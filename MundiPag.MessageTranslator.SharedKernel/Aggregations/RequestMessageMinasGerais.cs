using MundiPag.MessageTranslator.SharedKernel.Compositions.MinasGerais;
using MundiPag.MessageTranslator.SharedKernel.Contracts;

namespace MundiPag.MessageTranslator.SharedKernel.Aggregations
{
    public class RequestMessageMinasGerais : IRequestMessageMinasGerais
    {
        public City[] Cities { get; set; }
    }
}
