using MundiPag.MessageTranslator.SharedKernel.Business.Compositions.Acre;

namespace MundiPag.MessageTranslator.SharedKernel.Business.Contracts
{
    public interface IRequestMessageAcre : IRequestMessage
    {
        City[] Cities { get; }
    }
}
