using MundiPag.MessageTranslator.SharedKernel.Business.Compositions.Rio;

namespace MundiPag.MessageTranslator.SharedKernel.Business.Contracts
{
    public interface IRequestMessageRio : IRequestMessage
    {
        Cidade[] Cidade { get; }
    }
}