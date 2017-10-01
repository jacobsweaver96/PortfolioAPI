using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Xml.XPath;

namespace PortfolioAPI.Util
{
    /// <summary>
    /// Util for getting service credentials
    /// </summary>
    public static class CredentialManager
    {
        /// <summary>
        /// Gets the credentials for a given connection from the applications crd.xml file (defined in web.config)
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static Credentials GetCredentials(string connectionString)
        {
            var path = HttpContext.Current.Server.MapPath($"{ConfigurationManager.AppSettings["CredentialsFileName"]}");
            var credentialDoc = new XPathDocument(path);
            var credentialNavigator = credentialDoc.CreateNavigator();
            var credential = new Credentials();

            var test = credentialNavigator.SelectSingleNode($"/credential[@connectionString='{connectionString}']/username");
            credential.UserId = credentialNavigator.SelectSingleNode($"/credential[@connectionString='{connectionString}']/username").Value;
            credential.Password = credentialNavigator.SelectSingleNode($"/credential[@connectionString='{connectionString}']/password").Value;

            return credential;
        }
    }
    
    /// <summary>
    /// Credentials. Pretty self-explanatory
    /// </summary>
    public class Credentials
    {
        /// <summary>
        /// User Id / Username
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}