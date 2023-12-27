using System;
using System.Collections.Generic;
using MFI; 

namespace MFI.Repository
{
    public interface IAccountRepository
    {
        IEnumerable<Account> AccountsByOwner(Guid ownerId);
    }
}
