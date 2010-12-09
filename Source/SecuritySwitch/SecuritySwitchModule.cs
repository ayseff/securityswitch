using System;
using System.Web;
using System.Web.Configuration;

using SecuritySwitch.Abstractions;
using SecuritySwitch.Configuration;


namespace SecuritySwitch {
	/// <summary>
	/// Evaluates each request for the need to switch to HTTP/HTTPS.
	/// </summary>
	public class SecuritySwitchModule : IHttpModule {
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
			context.Application[KeyNames.CachedSettings] = settings;
					
			// Hook the application's AcquireRequestState event.
			// * This ensures that the session ID is available for cookie-less session processing.
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
			var settings = (Settings)context.Application[KeyNames.CachedSettings];

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
			enforcer.EnsureRequestMatchesSecurity(wrappedRequest, wrappedResponse, expectedSecurity, settings);
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