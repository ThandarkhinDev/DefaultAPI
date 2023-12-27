//using Entities.ExtendedModels;
using MFI;
using System;
using System.Collections.Generic;

namespace MFI.Repository
{
    public interface IAdminRepository : IRepositoryBase<Admin>
    {
        IEnumerable<Admin> GetAdminByLoginName(string loginName);
        dynamic FindByID(int ID);
        dynamic GetAdmins();
        IEnumerable<dynamic> GetAdminLoginValidation(string username);
        int CheckDuplicateAdminName(int adminID, string adminName);
        int CheckDuplicateAdminLoginName(int adminID, string loginName);
        int CheckDuplicateAdminNRC(int adminID, string nrc);

    }
       
}
