// Copyright (c) Martin Bjorkstrom. All rights reserved.
// Licensed under the BSD 2-Clause License. See LICENSE in the project root for license information.

namespace NlayerApp.Domain 
{
	public abstract class Entity : Entity<int>
	{
		public override int GetHashCode()
        {
        	return !IsTransient() ? Id.Value : object.GetHashCode();
        }
	}

	public abstract class Entity<T>
	{
		private int? m_requestedHashCode;

		public T? Id 
		{ 
			get; 
			protected set;
		}

		public virtual bool IsTransient()
		{
			return !Id.HasValue;
		}

        public virtual void ChangeCurrentIdentity(T identity)
        {
            Id = identity;
        }

		public override bool Equals(object obj)
        {
        	var item = obj as Entity<T>;

            if (ReferenceEquals(item, null))
            {
                return false;
            }

            if (ReferenceEquals(item, this))
            {
                return true;
            }

            if (item.IsTransient() || IsTransient())
            {
                return false;
            }
            
            return Id.Value.Equals(item.Id.Value);
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!m_requestedHashCode.HasValue)
                {
                    m_requestedHashCode = Id.Value.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
                }

                return m_requestedHashCode.Value;
            }
            
            return base.GetHashCode();
        }

        public static bool operator ==(Entity<T> left, Entity<T> right)
        {
        	return Equals(left,null) ? Equals(right, null) : left.Equals(right);
        }

        public static bool operator !=(Entity<T> left, Entity<T> right)
        {
            return !(left == right);
        }
	}
}