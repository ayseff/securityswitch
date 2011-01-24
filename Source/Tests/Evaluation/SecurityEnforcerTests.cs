// =================================================================================
// Copyright © 2004-2011 Matt Sollars
// All rights reserved.
// 
// This code and information is provided "as is" without warranty of any kind,
// either expressed or implied, including, but not limited to, the implied 
// warranties of merchantability and/or fitness for a particular purpose.
// =================================================================================
using Moq;

using SecuritySwitch.Abstractions;
using SecuritySwitch.Configuration;
using SecuritySwitch.Evaluation;

using Xunit;


namespace SecuritySwitch.Tests.Evaluation {
	public class SecurityEnforcerTests {
		[Fact]
		public void GetUriForMatchedSecurityRequestReturnsNullIfRequestSecurityMatchesSpecifiedSecurity() {
			// Arrange.
			var mockRequest = new Mock<HttpRequestBase>();
			var mockResponse = new Mock<HttpResponseBase>();
			var settings = new Settings();
			var enforcer = new SecurityEnforcer();

			// Act.
			mockRequest.SetupGet(req => req.IsSecureConnection).Returns(true);
			var alreadySecuredRequest = enforcer.GetUriForMatchedSecurityRequest(mockRequest.Object, mockResponse.Object,
			                                                                    RequestSecurity.Secure, settings);

			mockRequest.SetupGet(req => req.IsSecureConnection).Returns(false);
			var alreadyInsecureRequest = enforcer.GetUriForMatchedSecurityRequest(mockRequest.Object, mockResponse.Object,
			                                                                      RequestSecurity.Insecure, settings);


			// Assert.
			Assert.Null(alreadySecuredRequest);
			Assert.Null(alreadyInsecureRequest);
		}
	}
}