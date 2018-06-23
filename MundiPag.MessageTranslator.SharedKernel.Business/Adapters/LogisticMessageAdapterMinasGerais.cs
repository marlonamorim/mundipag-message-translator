using MundiPag.MessageTranslator.SharedKernel.Business.Contracts;
using MundiPag.MessageTranslator.SharedKernel.Business.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundiPag.MessageTranslator.SharedKernel.Business.Adapters
{
    public class LogisticMessageAdapterMinasGerais<TRequestMessage, TLogisticMessage> : IMessageAdapter<TRequestMessage, TLogisticMessage>
        where TRequestMessage : IRequestMessageMinasGerais
        where TLogisticMessage : ILogisticMessage
    {
        public Task<IList<TLogisticMessage>> Adapt(TRequestMessage incoming)
        {
            IList<TLogisticMessage> logistics = new List<TLogisticMessage>();

            if (incoming.Cities is null || !incoming.Cities.Any()) return Task.Run(() => default(IList<TLogisticMessage>));

            foreach (var cityIncoming in incoming.Cities)
            {
                string city = cityIncoming.Name;
                int cityPopulation = cityIncoming.Population;
                IList<Aggregations.Neighborhood> neighborhoods = new List<Aggregations.Neighborhood>();

                cityIncoming.Neighborhoods.ToList().ForEach(n => {
                    neighborhoods.Add(new Aggregations.Neighborhood
                    {
                        Name = n.Name,
                        Population = n.Population
                    });
                });

                TLogisticMessage logistic = CustomActivator.New<TLogisticMessage>(city, cityPopulation, neighborhoods);

                logistics.Add(logistic);
            }

            return Task.Run(() => logistics);
        }
    }
}
