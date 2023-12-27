using System.Collections.Generic;
using System.Linq;
using System;
using Repository;

namespace MFI.Repository
{
    public class AdminlevelmenuRepository : RepositoryBase<Adminlevelmenu>, IAdminlevelmenuRepository
    {
        public AdminlevelmenuRepository(AppDb repositoryContext)
            : base(repositoryContext)
        {
        }

        public Adminlevelmenu GetAdminlevelmenuByAdminLevelIDAdminMenuID(int AdminLevelID, int AdminMenuID)
        {
            return FindByCondition(Adminlevelmenu => Adminlevelmenu.AdminLevelID.Equals(AdminLevelID) && Adminlevelmenu.AdminMenuID.Equals(AdminMenuID))
                     .FirstOrDefault();
        }
        
    }
}
