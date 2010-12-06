using System;
using System.IO;
using System.Security.Permissions;
using System.Web;


namespace SecuritySwitch.Abstractions {
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal),
	 AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class HttpPostedFileBase {
		public virtual int ContentLength {
			get { throw new NotImplementedException(); }
		}

		public virtual string ContentType {
			get { throw new NotImplementedException(); }
		}

		public virtual string FileName {
			get { throw new NotImplementedException(); }
		}

		public virtual Stream InputStream {
			get { throw new NotImplementedException(); }
		}

		public virtual void SaveAs(string filename) {
			throw new NotImplementedException();
		}
	}
}