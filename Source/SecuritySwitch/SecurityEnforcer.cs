using System;
using System.Text;

using SecuritySwitch.Abstractions;
using SecuritySwitch.Configuration;


namespace SecuritySwitch {
	public class SecurityEnforcer : ISecurityEnforcer {
		/// <summary>
		/// Gets any URI that ensures the specified request is being accessed by the proper protocol.
		/// </summary>
		/// <param name="request">The request to ensure proper access for.</param>
		/// <param name="response">The response to use if a redirection or other output is necessary.</param>
		/// <param name="security">The security setting to match.</param>
		/// <param name="settings">The settings used for any redirection.</param>
		/// <returns>A URI that ensures the requested resources matches the specified security; or null if the current request already does.</returns>
		public string GetUriForMatchedSecurityRequest(HttpRequestBase request, HttpResponseBase response, RequestSecurity security, Settings settings) {
			string targetUri = null;

			if (security == RequestSecurity.Secure && !request.IsSecureConnection || security == RequestSecurity.Insecure && request.IsSecureConnection) {
				// Determine the target protocol and get any base target URI from the settings.
				string targetProtocolScheme;
				string baseTargetUri;
				if (security == RequestSecurity.Secure) {
					targetProtocolScheme = Uri.UriSchemeHttps;
					baseTargetUri = settings.BaseSecureUri;
				} else {
					targetProtocolScheme = Uri.UriSchemeHttp;
					baseTargetUri = settings.BaseInsecureUri;
				}

				if (string.IsNullOrEmpty(baseTargetUri)) {
					// If there is no base target URI, just switch the protocol scheme of the current request's URI.
					// * The RawUrl property maintains any cookieless session ID that may be present, thus eliminating the need to call ApplyAppPathModifier.
					targetUri = targetProtocolScheme + Uri.SchemeDelimiter + request.Url.Authority + request.RawUrl;
				} else {
					// Build the appropriate URI.
					var uri = new StringBuilder(baseTargetUri);
					uri.Append(request.CurrentExecutionFilePath).Append(request.Url.Query);

					// Normalize the URI.
					uri.Replace("//", "/");

					targetUri = uri.ToString();
				}
			}

			return targetUri;
		}
	}
}