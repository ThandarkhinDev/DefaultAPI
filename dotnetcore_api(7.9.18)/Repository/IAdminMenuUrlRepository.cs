//using Entities.ExtendedModels;
using System;
using System.Collections.Generic;

namespace MFI.Repository
{
    public interface IAdminMenuUrlRepository : IRepositoryBase<AdminMenuUrl>
    {
        AdminMenuUrl GetAdminMenuUrlById(int AdminMenuUrlId);
        AdminMenuUrl GetAdminMenuUrlByServiceUrl(string ServiceUrl);
    }
}
