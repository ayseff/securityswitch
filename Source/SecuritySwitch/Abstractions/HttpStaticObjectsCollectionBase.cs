using System;
using System.Collections;
using System.IO;
using System.Security.Permissions;
using System.Web;


namespace SecuritySwitch.Abstractions {
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal),
	 AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class HttpStaticObjectsCollectionBase : ICollection {
		public virtual bool IsReadOnly {
			get { throw new NotImplementedException(); }
		}

		public virtual object this[string name] {
			get { throw new NotImplementedException(); }
		}

		public virtual bool NeverAccessed {
			get { throw new NotImplementedException(); }
		}

		#region ICollection Members

		public virtual void CopyTo(Array array, int index) {
			throw new NotImplementedException();
		}

		public virtual IEnumerator GetEnumerator() {
			throw new NotImplementedException();
		}

		public virtual int Count {
			get { throw new NotImplementedException(); }
		}

		public virtual bool IsSynchronized {
			get { throw new NotImplementedException(); }
		}

		public virtual object SyncRoot {
			get { throw new NotImplementedException(); }
		}

		#endregion

		public virtual object GetObject(string name) {
			throw new NotImplementedException();
		}

		public virtual void Serialize(BinaryWriter writer) {
			throw new NotImplementedException();
		}
	}
}