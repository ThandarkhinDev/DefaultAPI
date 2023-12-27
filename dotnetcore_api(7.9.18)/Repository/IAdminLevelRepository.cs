//using Entities.ExtendedModels;
using MFI;
using System;
using System.Collections.Generic;

namespace MFI.Repository
{
    public interface IAdminLevelRepository : IRepositoryBase<Adminlevel>
    {
        IEnumerable<Adminlevel> GetAdminLevelByID(int adminLevelID);
        IEnumerable<Adminlevelmenu> GetAdminLevelMenuBylID(int adminLevelID);
        Adminlevel FindAdminLevel(int adminLevelID);
        int checkDuplicateAdminLevel(int adminLevelID, string adminLevel);
        dynamic GetAdminMenu(int chk);
        void DeleteAdminlevelMenu (int AdminLevelID);
        void AddAdminlevelmenu (List<Adminlevelmenu> newlist);

    }
       
}
