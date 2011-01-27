// =================================================================================
// Copyright © 2004-2011 Matt Sollars
// All rights reserved.
// 
// This code and information is provided "as is" without warranty of any kind,
// either expressed or implied, including, but not limited to, the implied 
// warranties of merchantability and/or fitness for a particular purpose.
// =================================================================================
using System;
using System.Web;

using SecuritySwitch.Configuration;


namespace SecuritySwitch {
	public class BeforeEvaluateRequestEventArgs : EventArgs {
		/// <summary>
		/// Gets the HttpApplication used to evaluate the request.
		/// </summary>
		public HttpApplication Application { get; private set; }

		/// <summary>
		/// Gets or sets a flag indicating whether or not to cancel the evaluation.
		/// </summary>
		public bool CancelEvaluation { get; set; }

		/// <summary>
		/// Gets the Settings used to evaluate the request.
		/// </summary>
		public Settings Settings { get; private set; }


		/// <summary>
		/// Creates an instance of BeforeEvaluateRequestEventArgs with the specified application and settings.
		/// </summary>
		/// <param name="application">The HttpApplication for the current context.</param>
		/// <param name="settings">An instance of Settings used for the evaluation of the request.</param>
		public BeforeEvaluateRequestEventArgs(HttpApplication application, Settings settings) {
			CancelEvaluation = false;
			Application = application;
			Settings = settings;
		}
	}
}