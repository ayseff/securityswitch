using System;

using SecuritySwitch.Abstractions;
using SecuritySwitch.Configuration;


namespace SecuritySwitch {
	public class SecurityEnforcer : ISecurityEnforcer {
		/// <summary>
		/// Ensures the specified request is being accessed by the proper protocol; redirecting as necessary.
		/// </summary>
		/// <param name="request">The request to ensure proper access for.</param>
		/// <param name="response">The response to use if a redirection or other output is necessary.</param>
		/// <param name="security">The security setting to match.</param>
		/// <param name="settings">The settings used for any redirection.</param>
		public void EnsureRequestMatchesSecurity(HttpRequestBase request, HttpResponseBase response, RequestSecurity security, Settings settings) {
			string targetUri = null;

			if (security == RequestSecurity.Secure && !request.IsSecureConnection) {
				// Build a URL for the secure version of the current request.
				if (string.IsNullOrEmpty(settings.BaseSecureUri)) {
					targetUri = SwitchProtocolScheme(Uri.UriSchemeHttps + Uri.SchemeDelimiter + request.Url.Authority + request.RawUrl);
				}


			} else if (security == RequestSecurity.Insecure && request.IsSecureConnection) {
				// Build a URL for the insecure version of the current request.
				if (string.IsNullOrEmpty(settings.BaseInsecureUri)) {
					targetUri = SwitchProtocolScheme(Uri.UriSchemeHttp + Uri.SchemeDelimiter + request.Url.Authority + request.RawUrl);
				}
			}


			if (targetUri != null) {
				// Redirect to any specified target URI.
				response.Redirect(targetUri);
			}
		}


		/// <summary>
		/// Switches the protocol scheme for the specified URI (from HTTP/HTTPS to the other).
		/// </summary>
		/// <param name="uri">The URI to switch protocol schemes for.</param>
		/// <returns>The same URI with the protocol scheme switched to HTTPS if the original was HTTP or vice versa.</returns>
		private string SwitchProtocolScheme(string uri) {
			var protocolScheme = (uri.StartsWith(Uri.UriSchemeHttps) ? Uri.UriSchemeHttps : Uri.UriSchemeHttp);
			return uri.Replace(protocolScheme, (protocolScheme == Uri.UriSchemeHttp ? Uri.UriSchemeHttps : Uri.UriSchemeHttp));
		}
	}
}