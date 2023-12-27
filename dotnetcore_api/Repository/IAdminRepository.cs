//using Entities.ExtendedModels;
using MFI;
using System;
using System.Collections.Generic;

namespace MFI.Repository
{
    public interface IAdminRepository
    {
        IEnumerable<Admin> GetAllAdmin();
        IEnumerable<Admin> GetAdminByLoginName(string loginName);
        void AddAdmin(Admin admin);
        void UpdateAdmin(Admin admin);
        void DeleteAdmin(Admin admin);
        dynamic FindByID(int ID);
        dynamic GetAdmins();
        IEnumerable<dynamic> GetAdminLoginValidation(string username);
        int CheckDuplicateAdminName(int adminID, string adminName);
        int CheckDuplicateAdminLoginName(int adminID, string loginName);
        int CheckDuplicateAdminNRC(int adminID, string nrc);

    }
       
}
