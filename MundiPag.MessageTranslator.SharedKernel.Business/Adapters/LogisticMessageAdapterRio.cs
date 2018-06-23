﻿using MundiPag.MessageTranslator.SharedKernel.Business.Contracts;
using MundiPag.MessageTranslator.SharedKernel.Business.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundiPag.MessageTranslator.SharedKernel.Business.Adapters
{
    public class LogisticMessageAdapterRio<TRequestMessage, TLogisticMessage> : IMessageAdapter<TRequestMessage, TLogisticMessage>
        where TRequestMessage : IRequestMessageRio
        where TLogisticMessage : ILogisticMessage
    {
        public Task<IList<TLogisticMessage>> Adapt(TRequestMessage incoming)
        {
            IList<TLogisticMessage> logistics = new List<TLogisticMessage>();

            if (incoming.Cidade is null || !incoming.Cidade.Any()) return Task.Run(() => default(IList<TLogisticMessage>));

            foreach (var cityIncoming in incoming.Cidade)
            {
                string city = cityIncoming.Nome;
                int cityPopulation = cityIncoming.Populacao;
                IList<Aggregations.Neighborhood> neighborhoods = new List<Aggregations.Neighborhood>();

                cityIncoming.Bairros.ToList().ForEach(n => {
                    neighborhoods.Add(new Aggregations.Neighborhood
                    {
                        Name = n.Nome,
                        Population = n.Populacao
                    });
                });

                TLogisticMessage logistic = CustomActivator.New<TLogisticMessage>(city, cityPopulation, neighborhoods);

                logistics.Add(logistic);
            }

            return Task.Run(() => logistics);
        }
    }
}
