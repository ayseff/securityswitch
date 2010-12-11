using SecuritySwitch.Abstractions;
using SecuritySwitch.Configuration;


namespace SecuritySwitch.Evaluation {
	/// <summary>
	/// The contract for a request evaluator.
	/// </summary>
	public interface IRequestEvaluator {
		/// <summary>
		/// Evaluates the specified request for the need to switch its security.
		/// </summary>
		/// <param name="request">The request to evaluate.</param>
		/// <param name="settings">The settings to use for evaluation.</param>
		/// <return>
		/// A RequestSecurity value indicating the security the evaluated request should be under.
		/// </return>
		RequestSecurity Evaluate(HttpRequestBase request, Settings settings);
	}
}