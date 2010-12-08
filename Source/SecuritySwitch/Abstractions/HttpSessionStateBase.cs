namespace System.Web
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Reflection;
    using System.Security.Permissions;
    using System.Web.SessionState;

    [AspNetHostingPermission(SecurityAction.LinkDemand, Level=AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level=AspNetHostingPermissionLevel.Minimal)]
    public abstract class HttpSessionStateBase : ICollection, IEnumerable
    {
        protected HttpSessionStateBase()
        {
        }

        public virtual void Abandon()
        {
            throw new NotImplementedException();
        }

        public virtual void Add(string name, object value)
        {
            throw new NotImplementedException();
        }

        public virtual void Clear()
        {
            throw new NotImplementedException();
        }

        public virtual void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(string name)
        {
            throw new NotImplementedException();
        }

        public virtual void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public virtual void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public virtual int CodePage
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual System.Web.HttpSessionStateBase Contents
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual HttpCookieMode CookieMode
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool IsCookieless
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool IsNewSession
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual bool IsSynchronized
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual object this[string name]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual object this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual NameObjectCollectionBase.KeysCollection Keys
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual int LCID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual SessionStateMode Mode
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual string SessionID
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual System.Web.HttpStaticObjectsCollectionBase StaticObjects
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual object SyncRoot
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual int Timeout
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}

