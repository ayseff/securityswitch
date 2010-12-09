using SecuritySwitch.Abstractions;
using SecuritySwitch.Configuration;


namespace SecuritySwitch {
	/// <summary>
	/// A contract for an enforcer of request security.
	/// </summary>
	public interface ISecurityEnforcer {
		/// <summary>
		/// Ensures the specified request is being accessed by the proper protocol; redirecting as necessary.
		/// </summary>
		/// <param name="request">The request to ensure proper access for.</param>
		/// <param name="response">The response to use if a redirection or other output is necessary.</param>
		/// <param name="security">The security setting to match.</param>
		/// <param name="settings">The settings used for any redirection.</param>
		void EnsureRequestMatchesSecurity(HttpRequestBase request, HttpResponseBase response, RequestSecurity security, Settings settings);
	}
}