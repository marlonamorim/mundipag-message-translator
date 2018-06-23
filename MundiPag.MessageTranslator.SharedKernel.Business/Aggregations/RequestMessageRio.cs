using MundiPag.MessageTranslator.SharedKernel.Business.Compositions.Rio;
using MundiPag.MessageTranslator.SharedKernel.Business.Contracts;

namespace MundiPag.MessageTranslator.SharedKernel.Business.Aggregations
{
    public class RequestMessageRio : IRequestMessageRio
    {
        public Cidade[] Cidade { get; set; }
    }
}
