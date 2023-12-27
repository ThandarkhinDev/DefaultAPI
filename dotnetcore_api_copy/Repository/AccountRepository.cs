using System;
using System.Collections.Generic;
using MFI;
using Repository;

namespace MFI.Repository
{
    public class AccountRepository: RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(AppDb repositoryContext)
            :base(repositoryContext)
        {
        }

        public IEnumerable<Account> AccountsByOwner(Guid ownerId)
        {
            return FindByCondition(a => a.OwnerId.Equals(ownerId));
        }
    }
}
