//using Entities.ExtendedModels;
using System;
using System.Collections.Generic;

namespace MFI.Repository
{
    public interface IAdminlevelmenuRepository
    {
        Adminlevelmenu GetAdminlevelmenuByAdminLevelIDAdminMenuID(int AdminLevelID, int AdminMenuID);
    }
}
