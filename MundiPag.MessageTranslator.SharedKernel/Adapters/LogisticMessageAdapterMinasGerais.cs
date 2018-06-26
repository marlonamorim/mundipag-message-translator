using MundiPag.MessageTranslator.SharedKernel.Aggregations;
using MundiPag.MessageTranslator.SharedKernel.Contracts;
using MundiPag.MessageTranslator.SharedKernel.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundiPag.MessageTranslator.SharedKernel.Adapters
{
    public class LogisticMessageAdapterMinasGerais<TRequestMessage, TLogisticMessage> : IMessageAdapter<TRequestMessage, TLogisticMessage>
        where TRequestMessage : IRequestMessageMinasGerais
        where TLogisticMessage : ILogisticMessage
    {
        public Task<IList<TLogisticMessage>> Adapt(TRequestMessage incoming)
        {
            IList<TLogisticMessage> logistics = new List<TLogisticMessage>();

            var result = Task.Run(() => {

                if (incoming.Cities is null || !incoming.Cities.Any()) return default(IList<TLogisticMessage>);

                foreach (var cityIncoming in incoming.Cities)
                {
                    string city = cityIncoming.Name;
                    int cityPopulation = cityIncoming.Population;
                    IList<Neighborhood> neighborhoods = new List<Neighborhood>();

                    cityIncoming.Neighborhoods.ToList().ForEach(n => {
                        neighborhoods.Add(new Neighborhood
                        {
                            Name = n.Name,
                            Population = n.Population
                        });
                    });

                    TLogisticMessage logistic = CustomActivator.New<TLogisticMessage>(city, cityPopulation, neighborhoods);

                    logistics.Add(logistic);
                }

                return logistics;

            });

            return result;
        }
    }
}
