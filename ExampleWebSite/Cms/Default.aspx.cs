using System;
using System.Text;
using System.Web;
using System.Xml.XPath;


namespace ExampleWebSite.Cms {
	public partial class Default : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			if (!Page.IsPostBack) {
				RenderPageContent();
				RenderMetaInformation();
			}
		}

		private void RenderPageContent() {
			PageInfo pageInfo = null;

			// Check for a specified page ID.
			var pageIdValue = Request.QueryString["pageId"];
			if (!string.IsNullOrEmpty(pageIdValue)) {
				int pageId;
				if (int.TryParse(pageIdValue, out pageId)) {
					pageInfo = QueryCmsContent(string.Format("/pages/page[@id={0}]", pageId));
				}
			}

			if (pageInfo == null) {
				// Check for a specified page slug.
				var pageSlug = Request.QueryString["pageSlug"];
				if (!string.IsNullOrEmpty(pageSlug)) {
					pageInfo = QueryCmsContent(string.Format("/pages/page[@slug='{0}']", pageSlug));
				}
			}

			if (pageInfo == null) {
				Title = "Page Not Found";
				litTitle.Text = Title;
				litContent.Text = "<p>The requested page was not found. Yeah, we could've returned a 404 error, but we didn't; did we?</p>";
			} else {
				Title = pageInfo.Title;
				litTitle.Text = Title;
				litContent.Text = pageInfo.Content;
			}
		}

		private void RenderMetaInformation() {
			// Build a list of all the meta information from the query string.
			var builder = new StringBuilder();
			foreach (var key in Request.QueryString.AllKeys) {
				builder.AppendFormat("<li>{0}: {1}</li>", key, Request.QueryString[key]);
			}

			litMeta.Text = string.Format("<ul>{0}</ul>", builder.ToString());
		}


		private PageInfo QueryCmsContent(string xPathQuery) {
			// Load the CMS content document.
			var cmsContent = new XPathDocument(MapPath("~/App_Data/CmsContent.xml"));
			var navigator = cmsContent.CreateNavigator();

			// Get any matching page node.
			var matchingPageNode = navigator.SelectSingleNode(xPathQuery);
			return (matchingPageNode == null ? null : GetPageInfoFromNavigator(matchingPageNode));
		}

		private static PageInfo GetPageInfoFromNavigator(XPathNavigator matchingPageNode) {
			// Create the PageInfo from the element navigator.
			var pageInfo = new PageInfo {
				Id = int.Parse(matchingPageNode.GetAttribute("id", string.Empty)),
				Slug = matchingPageNode.GetAttribute("slug", string.Empty),
				Title = matchingPageNode.GetAttribute("title", string.Empty),
				Content = HttpUtility.HtmlDecode(matchingPageNode.Value)
			};
			return pageInfo;
		}


		private class PageInfo {
			public int Id { get; set; }
			public string Slug { get; set; }
			public string Title { get; set; }
			public string Content { get; set; }
		}
	}
}