using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;

namespace AspNetCore.Authentication.Basic
{
    public class FailedContext : ResultContext<BasicOptions>
    {
        public FailedContext(HttpContext context, AuthenticationScheme scheme, BasicOptions options)
            : base(context, scheme, options) { }

        public Exception Exception { get; set; }
    }
}
