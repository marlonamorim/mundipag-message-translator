using MundiPag.MessageTranslator.SharedKernel.Business.Compositions.Acre;
using MundiPag.MessageTranslator.SharedKernel.Business.Contracts;

namespace MundiPag.MessageTranslator.SharedKernel.Business.Aggregations
{
    public class RequestMessageAcre : IRequestMessageAcre
    {
        public City[] Cities { get; set; }
    }
}
