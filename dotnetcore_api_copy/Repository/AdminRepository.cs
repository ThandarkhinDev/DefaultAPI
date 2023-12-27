using System.Collections.Generic;
using System.Linq;
using System;
using Repository;

namespace MFI.Repository
{
    public class AdminRepository: RepositoryBase<Admin>, IAdminRepository
    {
        public AdminRepository(AppDb repositoryContext):base(repositoryContext)
        {
            //this.RepositoryContext = repositoryContext;
        }

        public IEnumerable<Admin> GetAllAdmin() {
            return FindAll();
        }
        public dynamic FindByID(int id) {
            var adminObj = RepositoryContext.Admin.Find(id);
            if(adminObj != null) {
                 adminObj.CopyOldObj();
            }
            return adminObj;
        }
        public IEnumerable<Admin> GetAdminByLoginName(string loginName) {
            return FindByCondition(x => x.LoginName == loginName);
        }

        public int CheckDuplicateAdminName(int adminID, string adminName) {
            return FindByCondition(x => x.AdminID != adminID && x.AdminName == adminName).Count();
        }
        public int CheckDuplicateAdminLoginName(int adminID, string loginName) {
            return FindByCondition(x => x.AdminID != adminID && x.LoginName == loginName).Count();
        }
        public int CheckDuplicateAdminNRC(int adminID, string nrc) {
            return FindByCondition(x => x.AdminID != adminID &&  x.nrc == nrc).Count();
        }
        public dynamic GetAdmins() {
            var mainQuery = (from main in RepositoryContext.Admin
                                join s in RepositoryContext.State on main.state equals s.stateid into tmp
                                from tmps in tmp.DefaultIfEmpty()
                            select new {
                                main.AdminID,
                                main.AdminLevelID,
                                main.AdminName,
                                main.LoginName,
                                main.address,
                                main.phone,
                                main.nrc,
                                main.Email,
                                main.ImagePath,
                                stateId = main.state,
                                state = tmps != null ? tmps.statename : ""
                            });
            return mainQuery;
        }
        
        public IEnumerable<dynamic> GetAdminLoginValidation(string username) {
            return (from usr in RepositoryContext.Admin
                        join ul in RepositoryContext.Adminlevel on usr.AdminLevelID equals ul.AdminLevelID into tmp
                        from c in tmp
                        where usr.LoginName == (string)username
                        select new
                        {
                            usr.Password,
                            usr.Salt,
                            usr.AdminID,
                            usr.AdminName,
                            usr.AdminLevelID,
                            usr.login_fail_count,
                            usr.access_status,
                            usr.Email,
                            usr.ImagePath,
                            usr.LoginName,
                            c.restricted_iplist
                        });
        }
        public void AddAdmin(Admin admin) {
            Create(admin);
            Save();
        }

        public void UpdateAdmin(Admin admin) {
            Update(admin);
            Save();
        }

        public void DeleteAdmin(Admin admin) {
            Delete(admin);
            Save();
        }
    }
}
