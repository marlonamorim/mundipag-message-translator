using MundiPag.MessageTranslator.SharedKernel.Compositions.Rio;

namespace MundiPag.MessageTranslator.SharedKernel.Contracts
{
    public interface IRequestMessageRio : IRequestMessage
    {
        Cidade[] Cidade { get; }
    }
}