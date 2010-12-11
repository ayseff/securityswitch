using SecuritySwitch.Evaluation;

using Xunit;


namespace SecuritySwitch.Tests.Evaluation {
	public class StartsWithPathMatcherTests {
		[Fact]
		public void IsMatchWithCaseSensitiveReturnsTrueForMissingQuery() {
			// Arrange.
			const string Path = "/WebApp/Administration/Test.aspx?Param1=42&Param2=No";
			const string Pattern = "/WebApp/Administration/Test.aspx";

			var matcher = new StartsWithPathMatcher();

			// Act.
			var result = matcher.IsMatch(Path, Pattern, false);

			// Assert.
			Assert.True(result);
		}

		[Fact]
		public void IsMatchWithCaseInsensitiveReturnsTrueForEquivalentMissingQuery() {
			// Arrange.
			const string Path = "/WebApp/Administration/Test.aspx?Param1=42&Param2=No";
			const string Pattern = "/webapp/administration/test.aspx";

			var matcher = new StartsWithPathMatcher();

			// Act.
			var result = matcher.IsMatch(Path, Pattern, true);

			// Assert.
			Assert.True(result);
		}

		[Fact]
		public void IsMatchWithCaseSensitiveReturnsFalseForEquivalentMissingQuery() {
			// Arrange.
			const string Path = "/WebApp/Administration/Test.aspx?Param1=42&Param2=No";
			const string Pattern = "/webapp/administration/test.aspx";

			var matcher = new StartsWithPathMatcher();

			// Act.
			var result = matcher.IsMatch(Path, Pattern, false);

			// Assert.
			Assert.False(result);
		}

		[Fact]
		public void IsMatchWithCaseInsensitiveReturnsFalseForNearMatch() {
			// Arrange.
			const string Path = "/WebApp/Administration/Test.aspx?Param1=42&Param2=No";
			const string Pattern = "/webapp/administration/test.aspx?param1=43";

			var matcher = new StartsWithPathMatcher();

			// Act.
			var result = matcher.IsMatch(Path, Pattern, true);

			// Assert.
			Assert.False(result);
		}
	}
}