namespace SecuritySwitch.Redirection {
	/// <summary>
	/// A factory for ILocationRedirector.
	/// </summary>
	internal static class LocationRedirectorFactory {
		/// <summary>
		/// Gets the default location redirector.
		/// </summary>
		/// <returns></returns>
		internal static ILocationRedirector Create() {
			return new LocationRedirector();
		}
	}
}