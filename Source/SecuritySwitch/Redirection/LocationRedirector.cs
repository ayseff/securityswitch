using SecuritySwitch.Abstractions;


namespace SecuritySwitch {
	/// <summary>
	/// The default implementation of ILocationRedirector.
	/// </summary>
	public class LocationRedirector : ILocationRedirector {
		/// <summary>
		/// Redirects to the specified URL.
		/// </summary>
		/// <param name="response">The response to use for redirection.</param>
		/// <param name="url">The URL to redirect to.</param>
		/// <param name="bypassSecurityWarning">If set to <c>true</c> security warnings will be bypassed.</param>
		public void Redirect(HttpResponseBase response, string url, bool bypassSecurityWarning) {
			if (bypassSecurityWarning) {
				// Clear the current response buffer.
				response.Clear();

				// Add a refresh header to the response for the new path.
				response.AddHeader("Refresh", string.Concat("0;URL=", url));

				// Also, add JavaScript to replace the current location as backup.
				response.Write("<html><head><title></title>");
				response.Write("<!-- <script language=\"javascript\">window.location.replace(\"");
				response.Write(url);
				response.Write("\");</script> -->");
				response.Write("</head><body></body></html>");
			} else {
				// Permanent redirect.
				response.StatusCode = 301;
				response.RedirectLocation = url;
			}

			// End the current response.
			response.End();
		}
	}
}