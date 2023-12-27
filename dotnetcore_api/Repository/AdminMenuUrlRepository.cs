using System.Collections.Generic;
using System.Linq;
using System;
using Repository;

namespace MFI.Repository
{
    public class AdminMenuUrlRepository : RepositoryBase<AdminMenuUrl>, IAdminMenuUrlRepository
    {
        public AdminMenuUrlRepository(AppDb repositoryContext)
            : base(repositoryContext)
        {
        }
       
        public AdminMenuUrl GetAdminMenuUrlById(int AdminMenuUrlId)
        {
            return FindByCondition(AdminMenuUrl => AdminMenuUrl.AdminMenuID.Equals(AdminMenuUrlId))
                    .FirstOrDefault();
        }

        public AdminMenuUrl GetAdminMenuUrlByServiceUrl(string ServiceUrl)
        {
            return FindByCondition(AdminMenuUrl => AdminMenuUrl.ServiceUrl.Equals(ServiceUrl))
                    .FirstOrDefault();
        }

        public void UpdateAdminMenuUrl(AdminMenuUrl AdminMenuUrl)
        {
            Update(AdminMenuUrl);
            Save();
        }
    }
}
