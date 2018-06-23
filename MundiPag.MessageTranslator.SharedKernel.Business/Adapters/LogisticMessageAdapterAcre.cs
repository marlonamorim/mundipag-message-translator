﻿using MundiPag.MessageTranslator.SharedKernel.Business.Contracts;
using MundiPag.MessageTranslator.SharedKernel.Business.Messaging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MundiPag.MessageTranslator.SharedKernel.Business.Adapters
{
    public class LogisticMessageAdapterAcre<TRequestMessage, TLogisticMessage> : IMessageAdapter<TRequestMessage, TLogisticMessage>
        where TRequestMessage : IRequestMessageAcre
        where TLogisticMessage : ILogisticMessage
    {
        public Task<IList<TLogisticMessage>> Adapt(TRequestMessage incoming)
        {
            IList<TLogisticMessage> logistics = new List<TLogisticMessage>();

            if (incoming.Cities is null) return Task.Run(() => default(IList<TLogisticMessage>));

            foreach (var cityIncoming in incoming.Cities)
            {
                string city = cityIncoming.Name;
                int cityPopulation = cityIncoming.Population;
                IList<Aggregations.Neighborhood> Neighborhoods = new List<Aggregations.Neighborhood>();

                cityIncoming.Neighborhoods.ToList().ForEach(n => {
                    Neighborhoods.Add(new Aggregations.Neighborhood {
                        Name = n.Name,
                        Population = n.Population
                    });
                });

                TLogisticMessage logistic = CustomActivator.New<TLogisticMessage>(city, cityPopulation, Neighborhoods);

                logistics.Add(logistic);
            }

            return Task.Run(() => logistics);
        }
    }
}