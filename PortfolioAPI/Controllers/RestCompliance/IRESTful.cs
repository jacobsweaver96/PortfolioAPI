using ApiModels.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PortfolioAPI.Controllers.Interfaces
{
    /// <summary>
    /// Interface for enforcing RESTful principles
    /// </summary>
    public interface IRESTful
    {
        /// <summary>
        /// Gets formatted response for return related endpoints
        /// </summary>
        /// <returns>Formatted api response</returns>
        ApiResponse Paths();
    }
}
