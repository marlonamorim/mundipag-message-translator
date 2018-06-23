using MundiPag.MessageTranslator.SharedKernel.Business.Compositions.MinasGerais;

namespace MundiPag.MessageTranslator.SharedKernel.Business.Contracts
{
    public interface IRequestMessageMinasGerais : IRequestMessage
    {
        City[] Cities { get; }
    }
}
