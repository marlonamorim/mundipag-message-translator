namespace MundiPag.MessageTranslator.Api.Authorization
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BasicAuthorizeAttribute : TypeFilterAttribute
    {
        public BasicAuthorizeAttribute()
            : base(typeof(BasicAuthorizeFilter))
        {
        }
    }
}
