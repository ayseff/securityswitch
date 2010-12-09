namespace SecuritySwitch {
	internal static class SecurityEnforcerFactory {
		/// <summary>
		/// Gets a security enforcer.
		/// </summary>
		/// <returns></returns>
		internal static ISecurityEnforcer GetSecurityEnforcer() {
			return new SecurityEnforcer();
		}
	}
}