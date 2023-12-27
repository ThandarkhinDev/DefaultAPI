//using Entities.ExtendedModels;
using System;
using System.Collections.Generic;

namespace MFI.Repository
{
    public interface IAdminMenuRepository
    {
        IEnumerable<dynamic> GetAdminMenu(int adminLevelID);
        IEnumerable<dynamic> GetAdminMenuByAdminLevel(int adminLevelID);
       
    }
}
