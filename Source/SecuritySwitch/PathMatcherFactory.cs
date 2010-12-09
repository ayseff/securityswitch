using System;
using System.Collections.Generic;

using SecuritySwitch.Configuration;


namespace SecuritySwitch {
	/// <summary>
	/// A factory for PathMatchers.
	/// </summary>
	internal static class PathMatcherFactory {
		/// <summary>
		/// Gets the cached path matchers.
		/// </summary>
		private static IDictionary<PathMatchType, IPathMatcher> CachedMatchers {
			get {
				var cachedMatchers =
					HttpContext.Current.Items[CachedMatchers] as IDictionary<PathMatchType, IPathMatcher>;
				if (cachedMatchers == null) {
					cachedMatchers = new Dictionary<PathMatchType, IPathMatcher>();
					HttpContext.Current.Items.Add(CachedMatchers, cachedMatchers);
				}

				return cachedMatchers;
			}
		}

		/// <summary>
		/// Gets the appropriate path matcher for the specified match type.
		/// </summary>
		/// <param name="matchType">The PathMatchType used to determine the appropriate path matcher.</param>
		/// <returns></returns>
		internal static IPathMatcher GetPathMatcher(PathMatchType matchType) {
			// Check the cache first.
			var cachedMatchers = CachedMatchers;
			if (cachedMatchers.ContainsKey(matchType)) {
				return cachedMatchers[matchType];
			}

			// Create the appropriate path matcher.
			IPathMatcher pathMatcher;
			switch (matchType) {
				case PathMatchType.Regex:
					pathMatcher = new RegexPathMatcher();
					break;
				
				case PathMatchType.StartsWith:
					pathMatcher = new StartsWithPathMatcher();
					break;

				case PathMatchType.Exact:
					pathMatcher = new ExactPathMatcher();
					break;

				default:
					throw new ArgumentOutOfRangeException("matchType");
			}

			// Cache the path matcher.
			cachedMatchers.Add(matchType, pathMatcher);

			return pathMatcher;
		}
	}
}