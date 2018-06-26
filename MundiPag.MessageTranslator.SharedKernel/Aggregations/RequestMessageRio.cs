using MundiPag.MessageTranslator.SharedKernel.Compositions.Rio;
using MundiPag.MessageTranslator.SharedKernel.Contracts;

namespace MundiPag.MessageTranslator.SharedKernel.Aggregations
{
    public class RequestMessageRio : IRequestMessageRio
    {
        public Cidade[] Cidade { get; set; }
    }
}
