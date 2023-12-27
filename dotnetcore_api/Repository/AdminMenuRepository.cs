using System.Collections.Generic;
using System.Linq;
using System;
using Repository;

namespace MFI.Repository
{
    public class AdminMenuRepository : RepositoryBase<Adminmenu>, IAdminMenuRepository
    {
        public AdminMenuRepository(AppDb repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<dynamic> GetAdminMenu(int adminLevelID) {
            
            return  ( from main in RepositoryContext.Adminmenu
                    where main.SrNo <= 1000
                    orderby main.ParentID, main.SrNo
                    select new {
                        main.AdminMenuID,main.ParentID,main.SrNo,main.AdminMenuName,main.Icon,main.ControllerName
                        }
                    ).Union(from detail in RepositoryContext.AdminMenuDetails 
                            select new { AdminMenuID = detail.MenuID,ParentID = 0 , SrNo = 10000,AdminMenuName = "",
                                    Icon = "",ControllerName = detail.ControllerName}
                    )
                    .ToList()
                    .Select(q => new
                    {
                        AdminLevelID = adminLevelID,
                        MenuID = q.AdminMenuID,
                        ParentID = q.ParentID,
                        SrNo = q.SrNo,
                        MenuName = q.AdminMenuName,
                        Icon = q.Icon,
                        ControllerName = q.ControllerName,
                        Permission = string.Join(",", (from US in RepositoryContext.Adminmenu
                                                        where US.ParentID == q.AdminMenuID && US.SrNo > 1000
                                                        select US.AdminMenuName).ToList())
                    });
        }
       
        public IEnumerable<dynamic> GetAdminMenuByAdminLevel(int adminLevelID) {
            
            return (from ULM in RepositoryContext.Adminlevelmenu
                    join M in RepositoryContext.Adminmenu on ULM.AdminMenuID equals M.AdminMenuID
                    where ULM.AdminLevelID == adminLevelID && M.SrNo <= 1000
                    orderby M.ParentID, M.SrNo
                    select new {
                        M.AdminMenuID,M.ParentID,M.SrNo,M.AdminMenuName,M.Icon,M.ControllerName
                        }
                    ).Union(from ULM in RepositoryContext.Adminlevelmenu
                            join detail in RepositoryContext.AdminMenuDetails  on ULM.AdminMenuID equals detail.MenuID
                            where ULM.AdminLevelID == adminLevelID
                            select new { AdminMenuID = detail.MenuID,ParentID = 0 , SrNo = 10000,AdminMenuName = "",
                                    Icon = "",ControllerName = detail.ControllerName}
                    ).ToList()
                    .Select(q => new
                    {
                        AdminLevelID = adminLevelID,
                        MenuID = q.AdminMenuID,
                        ParentID = q.ParentID,
                        SrNo = q.SrNo,
                        MenuName = q.AdminMenuName,
                        Icon = q.Icon,
                        ControllerName = q.ControllerName,
                        Permission = string.Join(",", (from UU in RepositoryContext.Adminlevelmenu
                                                        join US in RepositoryContext.Adminmenu on UU.AdminMenuID equals US.AdminMenuID
                                                        where US.ParentID == q.AdminMenuID && UU.AdminLevelID == adminLevelID && US.SrNo > 1000
                                                        select US.AdminMenuName).ToList())
                    });
        }
    }
}
