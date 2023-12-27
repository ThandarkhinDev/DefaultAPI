//using Entities.ExtendedModels;
using MFI;
using System;
using System.Collections.Generic;

namespace MFI.Repository
{
    public interface IAdminLevelRepository
    {
        IEnumerable<Adminlevel> GetAllAdminLevel();
        IEnumerable<Adminlevel> GetAdminLevelByID(int adminLevelID);
        IEnumerable<Adminlevelmenu> GetAdminLevelMenuBylID(int adminLevelID);
        Adminlevel FindAdminLevel(int adminLevelID);
        int checkDuplicateAdminLevel(int adminLevelID, string adminLevel);
        dynamic GetAdminMenu(int chk);
        void AddAdminLevel (Adminlevel AdminLevel);
        void UpdateAdminLevel(Adminlevel AdminLevel);
        void DeleteAdminLevel(Adminlevel AdminLevel);    
        void DeleteAdminlevelMenu (int AdminLevelID);
        void AddAdminlevelmenu (List<Adminlevelmenu> newlist);

    }
       
}
