using SecuritySwitch.Abstractions;
using SecuritySwitch.Configuration;


namespace SecuritySwitch.Evaluation {
	/// <summary>
	/// A contract for an enforcer of request security.
	/// </summary>
	public interface ISecurityEnforcer {
		/// <summary>
		/// Gets any URI that ensures the specified request is being accessed by the proper protocol.
		/// </summary>
		/// <param name="request">The request to ensure proper access for.</param>
		/// <param name="response">The response to use if a redirection or other output is necessary.</param>
		/// <param name="security">The security setting to match.</param>
		/// <param name="settings">The settings used for any redirection.</param>
		/// <returns>A URI that ensures the requested resources matches the specified security; or null if the current request already does.</returns>
		string GetUriForMatchedSecurityRequest(HttpRequestBase request, HttpResponseBase response, RequestSecurity security, Settings settings);
	}
}