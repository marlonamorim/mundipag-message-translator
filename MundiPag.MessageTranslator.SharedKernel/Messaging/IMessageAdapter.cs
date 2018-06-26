using System.Collections.Generic;
using System.Threading.Tasks;

namespace MundiPag.MessageTranslator.SharedKernel.Messaging
{
    public interface IMessageAdapter<TFrom, TTo>
    {
        Task<IList<TTo>> Adapt(TFrom incoming);
    }
}
