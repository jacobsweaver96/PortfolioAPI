using PortfolioAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PortfolioAPI.DataServices.DataAccessors
{
    internal static class DbCtxLifespanHelper
    {
        internal static async Task UseDataContext(string ctxStr, Func<PortfolioDBEntities, Task> exec)
        {
            using (var ctx = new PortfolioDBEntities(ctxStr))
            {
                await exec(ctx);
            }
        }

        internal static async Task<T> UseDataContext<T>(string ctxStr, Func<PortfolioDBEntities, Task<T>> exec)
        {
            T ret = default(T);

            using (var ctx = new PortfolioDBEntities(ctxStr))
            {
                ret = await exec(ctx);
            }

            return ret;
        }
    }
}