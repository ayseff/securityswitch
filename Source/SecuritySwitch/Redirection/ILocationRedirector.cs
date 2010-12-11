using SecuritySwitch.Abstractions;


namespace SecuritySwitch {
	/// <summary>
	/// A contract for a location redirector.
	/// </summary>
	public interface ILocationRedirector {
		/// <summary>
		/// Redirects to the specified URL.
		/// </summary>
		/// <param name="response">The response to use for redirection.</param>
		/// <param name="url">The URL to redirect to.</param>
		/// <param name="bypassSecurityWarning">If set to <c>true</c> security warnings will be bypassed.</param>
		void Redirect(HttpResponseBase response, string url, bool bypassSecurityWarning);
	}
}