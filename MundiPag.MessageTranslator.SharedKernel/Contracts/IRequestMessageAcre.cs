using MundiPag.MessageTranslator.SharedKernel.Compositions.Acre;

namespace MundiPag.MessageTranslator.SharedKernel.Contracts
{
    public interface IRequestMessageAcre : IRequestMessage
    {
        City[] Cities { get; }
    }
}
