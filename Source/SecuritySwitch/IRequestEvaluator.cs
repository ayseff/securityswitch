using SecuritySwitch.Abstractions;
using SecuritySwitch.Configuration;


namespace SecuritySwitch {
	/// <summary>
	/// The contract for a request evaluator.
	/// </summary>
	public interface IRequestEvaluator {
		/// <summary>
		/// Evaluates the specified request for the need to switch its security.
		/// </summary>
		/// <param name="request">The request to evaluate.</param>
		/// <param name="settings">The settings to use for evaluation.</param>
		void Evaluate(HttpRequestBase request, Settings settings);
	}
}