using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.XPath;


namespace ExampleWebSite {
	public partial class SiteMap : System.Web.UI.UserControl {
		private string _applicationDirectory;

		private string ApplicationDirectory {
			get {
				if (_applicationDirectory == null) {
					_applicationDirectory = MapPath("~/");
				}
				return _applicationDirectory;
			}
		}


		protected void Page_Load(object sender, EventArgs e) {
			if (!Page.IsPostBack) {
				LoadMenu(MapPath("~/"), menuSiteMap.Items);
			}
		}

		private void LoadMenu(string startDirectory, MenuItemCollection itemCollection) {
			// Add the files.
			var aspxFiles = Directory.GetFiles(startDirectory, "*.aspx");
			foreach (var aspxFile in aspxFiles) {
				var path = aspxFile.Remove(0, ApplicationDirectory.Length);
				itemCollection.Add(new MenuItem(Path.GetFileNameWithoutExtension(aspxFile), path, null, "~/" + path.Replace('\\', '/')));
			}

			// Add the directories.
			var directories = Directory.GetDirectories(startDirectory);
			foreach (var directory in directories) {
				var path = directory.Remove(0, ApplicationDirectory.Length);
				var menuItem = new MenuItem(directory.Remove(0, startDirectory.Length).Replace(@"\", string.Empty), path) {
					Selectable = false
				};

				if (menuItem.Depth == 0 && menuItem.Text == "Cms") {
					LoadCmsMenu(menuItem.ChildItems);
				} else {
					LoadMenu(directory, menuItem.ChildItems);
				}

				if (menuItem.ChildItems.Count > 0) {
					// Remove any Default.aspx files.
					foreach (MenuItem childItem in menuItem.ChildItems) {
						if (childItem.Text.Equals("Default", StringComparison.CurrentCultureIgnoreCase)) {
							menuItem.ChildItems.Remove(childItem);
							menuItem.Selectable = true;
							menuItem.NavigateUrl = VirtualPathUtility.AppendTrailingSlash("~/" + path.Replace('\\', '/'));
							break;
						}
					}

					itemCollection.Add(menuItem);
				}
			}
		}

		private void LoadCmsMenu(MenuItemCollection itemCollection) {
			const string CmsPagePath = "~/Cms/Default.aspx";

			// Show the CMS links by Id and by Slug.
			var byIdItem = new MenuItem("Pages by Id", "cms-pages-by-id") {
				Selectable = false
			};
			var bySlugItem = new MenuItem("Pages by Slug", "cms-pages-by-slug") {
				Selectable = false
			};

			// Load the CMS content document.
			var cmsContent = new XPathDocument(MapPath("~/App_Data/CmsContent.xml"));
			var navigator = cmsContent.CreateNavigator();

			// Get any matching page node.
			var pageNodes = navigator.Select("/pages/page[@id][@slug]");
			while (pageNodes.MoveNext()) {
				var pageId = int.Parse(pageNodes.Current.GetAttribute("id", string.Empty));
				var pageSlug = pageNodes.Current.GetAttribute("slug", string.Empty);
				var pageTitle = pageNodes.Current.GetAttribute("title", string.Empty);

				byIdItem.ChildItems.Add(new MenuItem(pageTitle,
				                                     string.Format("cms-page-id-{0}", pageId),
				                                     null,
				                                     CmsPagePath + GenerateRandomQueryParameters("pageId", pageId.ToString())));
				bySlugItem.ChildItems.Add(new MenuItem(pageTitle,
				                                       string.Format("cms-page-slug-{0}", pageSlug),
				                                       null,
				                                       CmsPagePath + GenerateRandomQueryParameters("pageSlug", pageSlug)));
			}


			// Add the new items to the collection, if any CMS pages were found.
			if (byIdItem.ChildItems.Count > 0) {
				itemCollection.Add(byIdItem);
				itemCollection.Add(bySlugItem);
			}
		}

		private static string GenerateRandomQueryParameters(string guaranteedParamName, string guaranteedParamValue) {
			var possibleParamsAndValues = new List<KeyValuePair<string, string[]>> {
				new KeyValuePair<string, string[]>("cache", new[] { "on", "off", "auto" }),
				new KeyValuePair<string, string[]>("environment", new[] { "Testing", "Development", "Staging", "Production" }),
				new KeyValuePair<string, string[]>("author", new[] { "Matt", "John", "Sally", "Bill", "Jennifer", "Melissa", "Michael" })
			};

			// Initialize the random engine.
			var randomGen = new Random();

			// How many random params will be used?
			var randomParamCount = randomGen.Next(possibleParamsAndValues.Count + 1);

			// Where will the guaranteed parameter go?
			var guaranteedParamIndex = randomGen.Next(randomParamCount + 1);
			
			// Pick which random parameters will be in the result.
			var parameters = new Dictionary<string, string>(randomParamCount + 1);
			for (var i = 0; i < randomParamCount; i++) {
				// Add the guaranteed parameter where needed.
				if (i == guaranteedParamIndex) {
					parameters.Add(guaranteedParamName, guaranteedParamValue);
					continue;
				}

				// Pick a random param that is not already chosen.
				var r = -1;
				while (r == -1 || parameters.ContainsKey(possibleParamsAndValues[r].Key)) {
					r = randomGen.Next(possibleParamsAndValues.Count);
				}

				// Choose a random value for the random param.
				var param = possibleParamsAndValues[r];
				var valueIndex = randomGen.Next(param.Value.Length);
				parameters.Add(param.Key, param.Value[valueIndex]);
			}

			
			// Build the parameters.
			var builder = new StringBuilder();
			builder.Append("?");
			foreach (var parameter in parameters) {
				builder.Append(parameter.Key).Append("=").Append(parameter.Value).Append("&");
			}

			return builder.ToString(0, builder.Length - 1);
		}
	}
}