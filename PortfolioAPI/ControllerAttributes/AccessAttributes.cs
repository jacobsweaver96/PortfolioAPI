using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortfolioAPI.ControllerAttributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class RequiresReadAccessAttribute : Attribute
    {
        public readonly bool RequiresRead = true;
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class RequiresWriteAccessAttribute : Attribute
    {
        public readonly bool RequiresWrite = true;
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class RequiresAdminAccessAttribute : Attribute
    {
        public readonly bool RequiresAdmin = true;
    }
}