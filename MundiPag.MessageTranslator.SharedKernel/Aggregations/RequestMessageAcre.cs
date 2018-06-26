using MundiPag.MessageTranslator.SharedKernel.Compositions.Acre;
using MundiPag.MessageTranslator.SharedKernel.Contracts;

namespace MundiPag.MessageTranslator.SharedKernel.Aggregations
{
    public class RequestMessageAcre : IRequestMessageAcre
    {
        public City[] Cities { get; set; }
    }
}
