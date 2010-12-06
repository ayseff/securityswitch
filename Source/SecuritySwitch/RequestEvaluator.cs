using System;

using SecuritySwitch.Abstractions;
using SecuritySwitch.Configuration;


namespace SecuritySwitch {
	/// <summary>
	/// The default implementation of IRequestEvaluator.
	/// </summary>
	public class RequestEvaluator : IRequestEvaluator {
		/// <summary>
		/// Evaluates the specified request for the need to switch its security.
		/// </summary>
		/// <param name="request">The request to evaluate.</param>
		/// <param name="settings">The settings to use for evaluation.</param>
		public void Evaluate(HttpRequestBase request, Settings settings) {
			throw new NotImplementedException();
		}
	}
}