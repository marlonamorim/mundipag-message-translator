using System;

namespace MundiPag.MessageTranslator.SharedKernel.Business.Adapters
{
    internal class CustomActivator
    {
        public static T New<T>(params object[] arguments)
        {
            return (T)Activator.CreateInstance(typeof(T), arguments);
        }
    }
}
