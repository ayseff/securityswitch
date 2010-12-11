using System;
using System.Web;
using System.Web.Configuration;

using SecuritySwitch.Abstractions;
using SecuritySwitch.Configuration;
using SecuritySwitch.Evaluation;


namespace SecuritySwitch {
	/// <summary>
	/// Evaluates each request for the need to switch to HTTP/HTTPS.
	/// </summary>
	public class SecuritySwitchModule : IHttpModule {
		const string CachedSettingsKey = "SecuritySwitch.Settings";


		/// <summary>
		/// Initializes a module and prepares it to handle requests.
		/// </summary>
		/// <param name="context">
		/// An <see cref="T:System.Web.HttpApplication"/> that provides access to the methods, properties, and events common to 
		/// all application objects within an ASP.NET application.
		/// </param>
		public void Init(HttpApplication context) {
			if (context == null) {
				return;
			}

			// Get the settings for the securitySwitch section.
			var settings = WebConfigurationManager.GetSection("securitySwitch") as Settings;
			if (settings == null || settings.Mode == Mode.Off) {
				return;
			}

			// Store the settings in application state for cached access on each request.
			context.Application[CachedSettingsKey] = settings;
					
			// Hook the application's AcquireRequestState event.
			// * This ensures that the session ID is available for cookie-less session processing. 
			// * It just is not possible (that I know of) to get the original URL requested when cookie-less sessions are used.
			//   The Framework uses RewritePath when the HttpContext is created to strip the Session ID from the request's 
			//   Path/Url. The rewritten URL is actually stored in an internal field of HttpRequest; short of reflection, 
			//   it's not attainable.
			context.AcquireRequestState += ProcessRequest;
		}

		/// <summary>
		/// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
		/// </summary>
		public void Dispose() {}


		private void ProcessRequest(object sender, EventArgs e) {
			// Cast the source as an HttpApplication instance.
			var context = sender as HttpApplication;
			if (context == null) {
				return;
			}

			// Retrieve the settings from application state.
			var settings = (Settings)context.Application[CachedSettingsKey];

			// Raise the BeforeEvaluateRequest event and check if a subscriber indicated to cancel the 
			// evaluation of the current request.
			var eventArgs = new BeforeEvaluateRequestEventArgs(context, settings);
			InvokeBeforeEvaluateRequest(eventArgs);
			if (eventArgs.CancelEvaluation) {
				return;
			}

			// Wrap the current request and response (for testability).
			HttpRequestBase wrappedRequest = new HttpRequestWrapper(context.Request);
			HttpResponseBase wrappedResponse = new HttpResponseWrapper(context.Response);

			// Evaluate this request with the configured settings.
			var evaluator = RequestEvaluatorFactory.GetRequestEvaluator();
			var expectedSecurity = evaluator.Evaluate(wrappedRequest, settings);
			if (expectedSecurity == RequestSecurity.Ignore) {
				// No action is needed for a result of Ignore.
				return;
			}

			// Ensure the request matches the expected security.
			var enforcer = SecurityEnforcerFactory.GetSecurityEnforcer();
			var targetUrl = enforcer.GetUriForMatchedSecurityRequest(wrappedRequest, wrappedResponse, expectedSecurity, settings);
			if (string.IsNullOrEmpty(targetUrl)) {
				// No action is needed if the security enforcer did not return a target URL.
				return;
			}

			// Redirect.
			var redirector = LocationRedirectorFactory.GetLocationRedirector();
			redirector.Redirect(wrappedResponse, HttpUtility.HtmlAttributeEncode(targetUrl), settings.BypassSecurityWarning);
		}


		/// <summary>
		/// Occurs just before the SecureSwitchModule evaluates the current request.
		/// </summary>
		public event BeforeEvaluateRequestEventHandler BeforeEvaluateRequest;

		/// <summary>
		/// Raises the BeforeEvaluateRequest event.
		/// </summary>
		/// <param name="args">The BeforeEvaluateRequestEventArgs used by any event handler(s).</param>
		protected void InvokeBeforeEvaluateRequest(BeforeEvaluateRequestEventArgs args) {
			var handler = BeforeEvaluateRequest;
			if (handler != null) {
				handler(this, args);
			}
		}
	}


	/// <summary>
	/// The delegate for handlers of the BeforeEvaluateRequest event.
	/// </summary>
	public delegate void BeforeEvaluateRequestEventHandler(object sender, BeforeEvaluateRequestEventArgs args);
}