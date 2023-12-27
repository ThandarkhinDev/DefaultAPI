using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MFI.Repository;

namespace MFI
{

    [Route("api/[controller]")]
    public class MenuController : BaseController
    {
        private IRepositoryWrapper _repositoryWrapper;
        public MenuController(IRepositoryWrapper RW)
        {
            _repositoryWrapper = RW;
        }

        [HttpGet("GetAdminLevelMenuData/{AdminLevelID}", Name = "GetAdminLevelMenuData")]
        [Authorize]
        public dynamic GetAdminLevelMenuData(int AdminLevelID)
        {
            List<dynamic> dd = new List<dynamic>();
            dynamic objresponse = null;
            if (CheckIsAdministrator(AdminLevelID))
            {
                objresponse = (_repositoryWrapper.AdminMenu.GetAdminMenu(AdminLevelID));
            }
            else
            {
                objresponse =_repositoryWrapper.AdminMenu.GetAdminMenuByAdminLevel(AdminLevelID);
            }
            dynamic objresponsedata = new { data = objresponse };
            return objresponsedata;
        }

        public bool CheckIsAdministrator(int AdminLevelID)
        {
            var objAdminlevel = _repositoryWrapper.AdminLevel.FindAdminLevel(AdminLevelID);
            if (objAdminlevel != null)
            {
                if (objAdminlevel.IsAdministrator)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
    }
}