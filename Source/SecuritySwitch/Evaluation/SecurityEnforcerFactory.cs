namespace SecuritySwitch.Evaluation {
	/// <summary>
	/// A factory for ISecurityEnforcer.
	/// </summary>
	internal static class SecurityEnforcerFactory {
		/// <summary>
		/// Gets a security enforcer.
		/// </summary>
		/// <returns></returns>
		internal static ISecurityEnforcer Create() {
			return new SecurityEnforcer();
		}
	}
}