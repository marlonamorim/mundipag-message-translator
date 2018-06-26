namespace MundiPag.MessageTranslator.Api.DependencyInjection
{
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using MundiPag.MessageTranslator.SharedKernel.Adapters;
    using MundiPag.MessageTranslator.SharedKernel.Messaging;
    using MundiPag.MessageTranslator.SharedKernel.Translate;

    public class IoCBuilder
    {
        public static IContainer Builder(IServiceCollection serviceDescriptors) {

            var builder = new ContainerBuilder();

            builder.Register(c => new TranslateJsonMetadataRio()).As<ITranslateJsonMetadataRio>().InstancePerLifetimeScope();
            builder.Register(c => new TranslateJsonMetadataMinasGerais()).As<ITranslateJsonMetadataMinasGerais>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(LogisticMessageAdapterAcre<,>)).As(typeof(IMessageAdapter<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(LogisticMessageAdapterRio<,>)).As(typeof(IMessageAdapter<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(LogisticMessageAdapterMinasGerais<,>)).As(typeof(IMessageAdapter<,>)).InstancePerLifetimeScope();

            builder.RegisterType<LoggerFactory>().As<ILoggerFactory>().InstancePerLifetimeScope();

            builder.Populate(serviceDescriptors);

            return builder.Build();
        }
    }
}
