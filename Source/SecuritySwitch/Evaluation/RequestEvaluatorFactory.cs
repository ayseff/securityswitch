namespace SecuritySwitch.Evaluation {
	internal static class RequestEvaluatorFactory {
		/// <summary>
		/// Gets a request evaluator.
		/// </summary>
		/// <returns></returns>
		internal static IRequestEvaluator GetRequestEvaluator() {
			return new RequestEvaluator();
		}
	}
}