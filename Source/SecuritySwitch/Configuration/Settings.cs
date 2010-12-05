using System.Configuration;


namespace SecuritySwitch.Configuration {
	/// <summary>
	/// The different modes the module works in.
	/// </summary>
	public enum Mode {
		/// <summary>
		/// Indicates that the module is "on" and all requests should be evaluated.
		/// </summary>
		On,

		/// <summary>
		/// Only remote requests will be evaluated.
		/// </summary>
		RemoteOnly,

		/// <summary>
		/// Only local requests will be evaluated.
		/// </summary>
		LocalOnly,

		/// <summary>
		/// The module is "off" and no evaluation should occur.
		/// </summary>
		Off
	}

	/// <summary>
	/// The possible types to match request paths by.
	/// </summary>
	public enum PathMatchType {
		/// <summary>
		/// A regular expression is used to match any request paths for action.
		/// </summary>
		Regex,

		/// <summary>
		/// The start of request paths are matched against (e.g. ~/Administration). All requests with a path that start with the specified path to match will have action taken.
		/// </summary>
		StartsWith,

		/// <summary>
		/// An exact match with a request path is needed for action to be considered.
		/// </summary>
		Exact
	}

	/// <summary>
	/// The different modes for bypassing security warnings.
	/// </summary>
	public enum SecurityWarningBypassMode {
		/// <summary>
		/// Always bypass security warnings when switching to an unencrypted page.
		/// </summary>
		AlwaysBypass,

		/// <summary>
		/// Only bypass security warnings when switching to an unencrypted page if the proper query parameter is present.
		/// </summary>
		BypassWithQueryParam,

		/// <summary>
		/// Never bypass security warnings when switching to an unencrypted page.
		/// </summary>
		NeverBypass
	}


	/// <summary>
	/// Contains the settings of a &lt;securitySwitch&gt; configuration section.
	/// </summary>
	public class Settings : ConfigurationSection {
		#region Properties

		/// <summary>
		/// Gets or sets the name of the query parameter that will indicate to the module to bypass
		/// any security warning if WarningBypassMode = BypassWithQueryParam.
		/// </summary>
		[ConfigurationProperty("bypassQueryParamName")]
		public string BypassQueryParamName {
			get { return (string)this["bypassQueryParamName"]; }
			set { this["bypassQueryParamName"] = value; }
		}

		/// <summary>
		/// Gets or sets the path to a URI for encrypted redirections, if any.
		/// </summary>
		[ConfigurationProperty("encryptedUri"), RegexStringValidator(@"^(?:|(?:https://)?[\w\-][\w\.\-,]*(?:\:\d+)?(?:/[\w\.\-]+)*/?)$")]
		public string EncryptedUri {
			get { return (string)this["encryptedUri"]; }
			set {
				if (!string.IsNullOrEmpty(value))
					this["encryptedUri"] = value;
				else
					this["encryptedUri"] = null;
			}
		}

		/// <summary>
		/// Gets or sets a flag indicating whether or not to maintain the current path when redirecting
		/// to a different host.
		/// </summary>
		[ConfigurationProperty("maintainPath", DefaultValue = true)]
		public bool MaintainPath {
			get { return (bool)this["maintainPath"]; }
			set { this["maintainPath"] = value; }
		}

		/// <summary>
		/// Gets or sets the mode indicating how the secure switch settings are handled.
		/// </summary>
		[ConfigurationProperty("mode", DefaultValue = Mode.On)]
		public Mode Mode {
			get { return (Mode)this["mode"]; }
			set { this["mode"] = value; }
		}

		/// <summary>
		/// Gets the collection of directory settings read from the configuration section.
		/// </summary>
		[ConfigurationProperty("directories")]
		public DirectorySettingCollection Directories {
			get { return (DirectorySettingCollection)this["directories"]; }
		}

		/// <summary>
		/// Gets the collection of file settings read from the configuration section.
		/// </summary>
		[ConfigurationProperty("files")]
		public FileSettingCollection Files {
			get { return (FileSettingCollection)this["files"]; }
		}

		/// <summary>
		/// Gets or sets the path to a URI for unencrypted redirections, if any.
		/// </summary>
		[ConfigurationProperty("unencryptedUri"), RegexStringValidator(@"^(?:|(?:http://)?[\w\-][\w\.\-,]*(?:\:\d+)?(?:/[\w\.\-]+)*/?)$")]
		public string UnencryptedUri {
			get { return (string)this["unencryptedUri"]; }
			set {
				if (!string.IsNullOrEmpty(value))
					this["unencryptedUri"] = value;
				else
					this["unencryptedUri"] = null;
			}
		}

		/// <summary>
		/// Gets or sets the bypass mode indicating whether or not to bypass security warnings
		/// when switching to a unencrypted page.
		/// </summary>
		[ConfigurationProperty("warningBypassMode", DefaultValue = SecurityWarningBypassMode.BypassWithQueryParam)]
		public SecurityWarningBypassMode WarningBypassMode {
			get { return (SecurityWarningBypassMode)this["warningBypassMode"]; }
			set { this["warningBypassMode"] = value; }
		}

		/// <summary>
		/// This property is for internal use and is not meant to be set.
		/// </summary>
		[ConfigurationProperty("xmlns")]
		public string XmlNamespace {
			get { return (string)this["xmlns"]; }
			set { this["xmlns"] = value; }
		}

		#endregion
	}
}