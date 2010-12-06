using System.Configuration;


namespace SecuritySwitch.Configuration {
	/// <summary>
	/// Contains the settings of a &lt;securitySwitch&gt; configuration section.
	/// </summary>
	public class Settings : ConfigurationSection {
		private const string UriValidationPattern = @"[\w\-][\w\.\-,]*(?:\:\d+)?(?:/[\w\.\-]+)*/?)$";

		#region Properties

		/// <summary>
		/// Gets or sets the path to a URI for unencrypted redirections, if any.
		/// </summary>
		[ConfigurationProperty(ElementNames.BaseInsecureUri), RegexStringValidator(@"^(?:|(?:http://)?" + UriValidationPattern)]
		public string BaseInsecureUri {
			get { return (string)this[ElementNames.BaseInsecureUri]; }
			set { this[ElementNames.BaseInsecureUri] = (!string.IsNullOrEmpty(value) ? value : null); }
		}

		/// <summary>
		/// Gets or sets the base path to a URI for encrypted redirections, if any.
		/// </summary>
		[ConfigurationProperty(ElementNames.BaseSecureUri), RegexStringValidator(@"^(?:|(?:https://)?" + UriValidationPattern)]
		public string BaseSecureUri {
			get { return (string)this[ElementNames.BaseSecureUri]; }
			set { this[ElementNames.BaseSecureUri] = (!string.IsNullOrEmpty(value) ? value : null); }
		}

		/// <summary>
		/// Gets or sets the name of the query parameter that will indicate to the module to bypass
		/// any security warning if WarningBypassMode is BypassWithQueryParam.
		/// </summary>
		[ConfigurationProperty(ElementNames.BypassQueryParamName)]
		public string BypassQueryParamName {
			get { return (string)this[ElementNames.BypassQueryParamName]; }
			set { this[ElementNames.BypassQueryParamName] = value; }
		}

		/// <summary>
		/// Gets or sets a flag indicating whether or not to maintain the current path when redirecting
		/// to a different host.
		/// </summary>
		[ConfigurationProperty(ElementNames.MaintainPath, DefaultValue = true)]
		public bool MaintainPath {
			get { return (bool)this[ElementNames.MaintainPath]; }
			set { this[ElementNames.MaintainPath] = value; }
		}

		/// <summary>
		/// Gets or sets the mode indicating how the secure switch settings are handled.
		/// </summary>
		[ConfigurationProperty(ElementNames.Mode, DefaultValue = Mode.On)]
		public Mode Mode {
			get { return (Mode)this[ElementNames.Mode]; }
			set { this[ElementNames.Mode] = value; }
		}

		/// <summary>
		/// Gets the collection of path settings read from the configuration section.
		/// </summary>
		[ConfigurationProperty(ElementNames.Paths, IsDefaultCollection = true, IsRequired = true)]
		public PathSettingCollection Paths {
			get { return (PathSettingCollection)this[ElementNames.Paths]; }
		}

		/// <summary>
		/// Gets or sets the bypass mode indicating whether or not to bypass security warnings
		/// when switching to a unencrypted page.
		/// </summary>
		[ConfigurationProperty(ElementNames.WarningBypassMode, DefaultValue = SecurityWarningBypassMode.BypassWithQueryParam)]
		public SecurityWarningBypassMode WarningBypassMode {
			get { return (SecurityWarningBypassMode)this[ElementNames.WarningBypassMode]; }
			set { this[ElementNames.WarningBypassMode] = value; }
		}


		/// <summary>
		/// This property is for internal use and is not meant to be set.
		/// </summary>
		[ConfigurationProperty(ElementNames.XmlNamespace)]
		protected string XmlNamespace {
			get { return (string)this[ElementNames.XmlNamespace]; }
			set { this[ElementNames.XmlNamespace] = value; }
		}

		#endregion
	}
}