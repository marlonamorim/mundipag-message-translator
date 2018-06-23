using System.Collections.Generic;
using System.Threading.Tasks;

namespace MundiPag.MessageTranslator.SharedKernel.Business.Messaging
{
    public interface IMessageAdapter<TFrom, TTo>
    {
        Task<IList<TTo>> Adapt(TFrom incoming);
    }
}
