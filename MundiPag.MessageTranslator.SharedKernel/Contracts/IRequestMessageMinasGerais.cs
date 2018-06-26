using MundiPag.MessageTranslator.SharedKernel.Compositions.MinasGerais;

namespace MundiPag.MessageTranslator.SharedKernel.Contracts
{
    public interface IRequestMessageMinasGerais : IRequestMessage
    {
        City[] Cities { get; }
    }
}
