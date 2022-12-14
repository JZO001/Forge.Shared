/* *********************************************************************
 * Date: 25 Oct 2012
 * Created by: Zoltan Juhasz
 * E-Mail: forge@jzo.hu
***********************************************************************/

#if NET40
#else
using Microsoft.Extensions.Configuration;
#endif
using System;

namespace Forge.Shared
{

    /// <summary>
    /// Represents an application with custom extensions
    /// WARNING: this service does not supported on platform 'browser'
    /// </summary>
    public static class ApplicationHelper
    {

        private static readonly string APP_CONFIG_ID = "ApplicationID";

        /// <summary>
        /// Represents the constant of the machine name
        /// </summary>
        public static readonly string MachineName = "$MachineName";

        /// <summary>
        /// Represents the constant of the user domain name
        /// </summary>
        public static readonly string UserDomainName = "$UserDomainName";

        /// <summary>
        /// Represents the constant of the user name
        /// </summary>
        public static readonly string UserName = "$UserName";

        /// <summary>
        /// Gets or sets the application id.
        /// WARNING: this method does not supported on platform 'browser'
        /// </summary>
        /// <value>
        /// The application id.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
        public static string ApplicationId
        {
            get
            {
                string
#if NET40 || NETSTANDARD2_0
#else
            ?
#endif
                    appId = System.Configuration.ConfigurationManager.AppSettings[APP_CONFIG_ID];
                return BuildAppId(appId);
            }
            set
            {
                System.Configuration.ConfigurationManager.AppSettings[APP_CONFIG_ID] = value;
            }
        }

#if NET40
#else
        /// <summary>Gets the application identifier from i configuration.</summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="System.ArgumentNullException">configuration</exception>
        public static string GetApplicationIdFromIConfiguration(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            string appId = configuration.GetValue<string>(APP_CONFIG_ID);
            return BuildAppId(appId);
        }
#endif

        private static string BuildAppId(string
#if NET40 || NETSTANDARD2_0
#else
            ?
#endif
            appId)
        {
            if (string.IsNullOrEmpty(appId))
            {
                throw new InitializationException("Unable to find application identifier in configuration.");
            }

            if (appId.Contains(MachineName))
            {
                appId = appId.Replace(MachineName, Environment.MachineName);
            }

            if (appId.Contains(UserDomainName))
            {
                appId = appId.Replace(UserDomainName, Environment.UserDomainName);
            }

            if (appId.Contains(UserName))
            {
                appId = appId.Replace(UserName, Environment.UserName);
            }

            return appId;
        }

    }

}
