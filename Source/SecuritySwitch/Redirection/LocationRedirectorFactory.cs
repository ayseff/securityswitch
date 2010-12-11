namespace SecuritySwitch {
	/// <summary>
	/// A factory for ILocationRedirector.
	/// </summary>
	internal static class LocationRedirectorFactory {
		/// <summary>
		/// Gets the default location redirector.
		/// </summary>
		/// <returns></returns>
		internal static ILocationRedirector GetLocationRedirector() {
			return new LocationRedirector();
		}
	}
}