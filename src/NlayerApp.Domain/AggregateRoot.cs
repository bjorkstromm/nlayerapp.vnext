// Copyright (c) Martin Bjorkstrom. All rights reserved.
// Licensed under the BSD 2-Clause License. See LICENSE in the project root for license information.

using System;

namespace NlayerApp.Domain 
{
	public abstract class AggregateRoot : Entity<Guid>
	{
		public override bool IsTransient()
        {
            return !Id.HasValue || Id == Guid.Empty;
        }

        public void GenerateNewIdentity()
        {
            if (IsTransient())
            {
                Id = Guid.NewGuid();
            }
        }

        public void ChangeCurrentIdentity(Guid identity)
        {
            if (identity != Guid.Empty)
            {
                Id = identity;
            }
        }
        
	}
}