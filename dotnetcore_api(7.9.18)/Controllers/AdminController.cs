using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using MFI.Repository;
namespace MFI
{

    [Route("api/[controller]")]
    public class AdminController : BaseController
    {
        private IRepositoryWrapper _repositoryWrapper;

        public AdminController(IRepositoryWrapper RW)
        {
            _repositoryWrapper = RW;
        }

        [HttpPost("GetAdminList", Name = "GetAdminList")]
        [Authorize]
        public dynamic GetAdminList([FromBody] Newtonsoft.Json.Linq.JObject param)
        {
            var mainQuery  = Enumerable.Repeat(new
            {
                AdminID = default(int),
                AdminLevelID = default(int),
                AdminName = default(string),
                LoginName = default(string),
                address = default(string),
                phone = default(string),
                nrc = default(string),
                Email = default(string),
                ImagePath = default(string),
                stateId = default(long),
                state = default(string) 
            }, 0).AsQueryable();

          /*   var objstr = HttpContext.Request.Form["formDataObj"];
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(objstr); */
            dynamic obj = param;   
            int currentPage = obj.skip;
            int rowsPerPage = obj.take;
            string sortField = null;
            string sortBy = "";
            if( obj.sort.Count > 0)
            {
                var sort =  obj.sort[0];
                sortBy = sort.dir == null ? sortBy : sort.dir.Value;
                sortField = sort.field.Value;
            }
            if(sortField == null || sortField == "")
                sortField = "AdminID";
            if(sortBy == null || sortBy == "")
                sortBy = "desc";
          
            mainQuery = _repositoryWrapper.Admin.GetAdmins(); // admin table + state 

            for(int i = 0 ; i < obj.filter.filters.Count;i++)
            {
                string filterName = obj.filter.filters[i].field;
                var filterValue = obj.filter.filters[i].value;
                if(filterName == "AdminID")
                {   int AdminID = filterValue;
                    mainQuery = mainQuery.Where(x=> x.AdminID == AdminID);
                }
                else if(filterName == "AdminName")
                {   string AdminName = filterValue;
                  mainQuery = mainQuery.Where(x => x.AdminName.Contains(AdminName));
                }
                else if(filterName == "phone")
                {
                    string phone = filterValue;
                   mainQuery = mainQuery.Where(x => x.phone == phone);
                }
                else if(filterName == "Email")
                {
                    string Email = filterValue;
                    mainQuery = mainQuery.Where(x => x.Email.Contains(Email));
                } 
                else if(filterName == "state")
                {
                    int stateid = filterValue;
                    mainQuery = mainQuery.Where(x => x.stateId == stateid);
                } 
            }

            var objSort = new SortModel();
            objSort.ColId = sortField;
            objSort.Sort = sortBy;
            var sortList = new System.Collections.Generic.List<SortModel>();
            sortList.Add(objSort);
            mainQuery = mainQuery.OrderBy(sortList); 
            if(mainQuery.Count() < currentPage + 1 ) currentPage = 0; // reset paging if total found count is less than current page number
            var tmpList = PaginatedList<dynamic>.Create(mainQuery,currentPage,rowsPerPage);
            var objtotal = tmpList.TotalRecords;
            
            var objresult = tmpList.Select(x=>
                new{
                    AdminID = x.AdminID,
                    AdminLevelID = x.AdminLevelID,
                    AdminName = x.AdminName,
                    LoginName = x.LoginName,
                    address = x.address,
                    phone = x.phone,
                    nrc = x.nrc,
                    Email = x.Email,
                    state = x.state,
                    stateid = x.stateId
                }
            ).ToList();

            dynamic objresponse = new { data = objresult, dataFoundRowsCount = objtotal };
            return objresponse;
        }

        [HttpGet("EncryptPWD/{PWD}", Name = "EncryptPWD")]
        public dynamic EncryptPWD(string PWD)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash. 
            
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(PWD));           
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < (data.Length); i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
            
                return sBuilder.ToString();
            }  
        }                   

        [HttpPost("GetAdminSetup/{AdminID}", Name = "GetAdminSetup")]
        [Authorize]
        public dynamic GetAdminSetup(int AdminID)
        {
            var Active = HttpContext.Request.Form["Active"];
            var Blocked = HttpContext.Request.Form["Blocked"];
            var mainQuery = _repositoryWrapper.Admin.FindAll();
            if (AdminID != 0)
            {
               mainQuery = mainQuery.Where(x => x.AdminID == AdminID);
            }

            if (Active == "true" && Blocked == "false")
            {
                var myInClause1 = new sbyte[] { 0 };
                mainQuery = mainQuery.Where(x => myInClause1.Contains(x.access_status));
            }
            else if (Active == "false" && Blocked == "true")
            {
                var myInClause1 = new sbyte[] { 1, 2 };
                mainQuery = mainQuery.Where(x => myInClause1.Contains(x.access_status));
            }
            else if (Active == "false" && Blocked == "false")
            {
                var myInClause1 = new sbyte[] { 1 };
                mainQuery = mainQuery.Where(x => myInClause1.Contains(x.access_status));
            }
            else
            {
                var myInClause2 = new sbyte[] { 0, 2 };
                mainQuery = mainQuery.Where(x => myInClause2.Contains(x.access_status));
            }
            dynamic objresponse = new { data = mainQuery.ToList() };
            return objresponse;
        }

        [HttpGet("GetAdminComboData", Name = "GetAdminComboData")]
        [Authorize]
        public dynamic GetAdminComboData()
        {
            var adminLevel = _repositoryWrapper.AdminLevel.FindAll();
            var state = _repositoryWrapper.State.GetAllActiveState();

            dynamic objresponse = new { adminLevel = adminLevel, state = state };
            return objresponse;
        }

        [HttpPost("checkDuplicate", Name = "AdmincheckDuplicate")]
        [Authorize]
        public dynamic checkDuplicate([FromBody] Newtonsoft.Json.Linq.JObject param)
        {
          /*   var objstr = HttpContext.Request.Form["formDataObj"];
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(objstr); */
            dynamic obj = param;
            dynamic objresult = null;
            int AdminID = obj.AdminID != null ? obj.AdminID : 0;
            string AdminName = obj.AdminName;
            string LoginName = obj.LoginName;
            string nrc = obj.nrc;

            var duplicateName = _repositoryWrapper.Admin.CheckDuplicateAdminName(AdminID, AdminName);
            var duplicateLoginName = _repositoryWrapper.Admin.CheckDuplicateAdminLoginName(AdminID, LoginName);
            var duplicateNrc = _repositoryWrapper.Admin.CheckDuplicateAdminNRC(AdminID,nrc);

            objresult = new {   
                                name = duplicateName > 0 ? 1 : 0, 
                                loginName = duplicateLoginName > 0 ? 1 : 0, 
                                nrc = duplicateNrc > 0 ? 1 : 0 
                            };

            List<dynamic> dd = new List<dynamic>();
            dd.Add(objresult);
            dynamic objresponse = new { data = dd };
            return objresponse;
        }

        [HttpPost("AddAdminSetup", Name = "AddAdminSetup")]
        [Authorize]
        public dynamic AddAdminSetup([FromBody] Newtonsoft.Json.Linq.JObject param)
        {
            /* var objstr = HttpContext.Request.Form["formDataObj"];
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(objstr); */
            dynamic obj = param;

            int AdminID = obj.AdminID != null ? obj.AdminID : 0;
            string Admin = obj.Admin;
          
            Admin objAdmin = _repositoryWrapper.Admin.FindByID(AdminID);
            if (objAdmin != null)
            {
                objAdmin.AdminName = obj.AdminName.Value;
                objAdmin.AdminLevelID = (int)obj.AdminLevelID;
                objAdmin.LoginName = obj.LoginName.Value;
                objAdmin.Email = obj.Email.Value;
                objAdmin.phone = obj.phone.Value;
                objAdmin.address = obj.address.Value;
                objAdmin.nrc = obj.nrc.Value;
                objAdmin.state = (int)obj.stateid;
                              
                _repositoryWrapper.Admin.Update(objAdmin);
                _repositoryWrapper.EventLog.Update("Update Admin", objAdmin);
            }
            else
            {
                Admin newobj = new Admin();
                newobj.AdminName = obj.AdminName;
                newobj.AdminLevelID = obj.AdminLevelID;
                newobj.LoginName = obj.LoginName;
                newobj.Email = obj.Email;
                newobj.access_status = 0;
                newobj.created_date = System.DateTime.Now;
                newobj.modified_date = System.DateTime.Now;
                newobj.phone = obj.phone;
                newobj.state = obj.stateid;
                newobj.nrc = obj.nrc;
                newobj.address = obj.address;

                var password = obj.Password;
                var settingresult = _repositoryWrapper.Setting.GetPasswordValidation().ToList();
                var pwdlength = settingresult[0].Value;

                if (password.ToString().Length < int.Parse(pwdlength))
                {
                    AdminID = -3;
                }
                else
                {
                    string salt = Operational.Encrypt.SaltedHash.GenerateSalt();
                    password = Operational.Encrypt.SaltedHash.ComputeHash(salt, password.ToString());
                    newobj.Password = password;
                    newobj.Salt = salt;
                    _repositoryWrapper.Admin.Create(newobj);
                    AdminID = newobj.AdminID;
                }
                _repositoryWrapper.EventLog.Insert("Add Admin", newobj);
            }

            dynamic objresponse = new { data = AdminID };
            return objresponse;
        }

        [HttpGet("ResetPassword/{AdminID}", Name = "AdminResetPassword")]
        [Authorize]
        public dynamic ResetPassword(int AdminID)
        {
            Admin objAdmin = _repositoryWrapper.Admin.FindByID(AdminID);
            string Password = "";
            string salt = "";
            string PWD = "";
            dynamic objresponse;
            objresponse = new { data = false };

            if (objAdmin != null)
            {
                //Password = "gwtsoft1"; //Operational.Cryptography.RandomPassword.Generate();
                var settingresult = (_repositoryWrapper.Setting.GetPasswordValidation()).ToList();
                var pwdlength = settingresult[0].Value;
                Password = Operational.Cryptography.RandomPassword.Generate(int.Parse(pwdlength));

                if (Password != "" && Password.Length >= int.Parse(pwdlength))
                {
                    salt = objAdmin.Salt;
                    PWD = Operational.Encrypt.SaltedHash.ComputeHash(salt, Password);
                    objAdmin.Password = PWD;
                    _repositoryWrapper.Admin.Update(objAdmin);

                    _repositoryWrapper.EventLog.Info("Reset the Password of Admin", "Success");
                    objresponse = new { data = Password };
                }
            }
            return objresponse;
        }

        [HttpPost("checkPassword", Name = "checkPassword")]
        [Authorize]
        public dynamic checkPassword([FromBody] Newtonsoft.Json.Linq.JObject param)
        {
            dynamic obj = param;
            string oldPwd = (string)obj.oldPassword; 
            int AdminID = int.Parse(_tokenData.UserID);
          //  string oldPwd = HttpContext.Request.Form["formDataObj"];
            var objAdmin = _repositoryWrapper.Admin.FindByID(AdminID);
            dynamic objresponse;

            if (objAdmin != null)
            {
                string oldhash = objAdmin.Password;
                string oldsalt = objAdmin.Salt;
                bool flag = Operational.Encrypt.SaltedHash.Verify(oldsalt, oldhash, oldPwd);
                objresponse = new { data = flag };
            }
            else
                objresponse = new { data = false };

            return objresponse;
        }

        [HttpPost("PassChange", Name = "PassChange")]
        [Authorize]
        public dynamic PassChange([FromBody] Newtonsoft.Json.Linq.JObject param)
        {
           dynamic obj = param;
            string oldPassword = obj.oldPassword;//HttpContext.Request.Form["formDataObj1"];
            string Password = obj.newPassword;//HttpContext.Request.Form["formDataObj2"];


            int AdminID = int.Parse(_tokenData.UserID);
            var settingresult = (_repositoryWrapper.Setting.GetPasswordValidation()).ToList();
            var pwdlength = settingresult[0].Value;
            dynamic objresponse;
            objresponse = new { data = false };

            if (Password.ToString().Length >= int.Parse(pwdlength))
            {
                Admin objAdmin = _repositoryWrapper.Admin.FindByID(AdminID);
                string salt = "";
                string PWD = "";

                if (objAdmin != null)
                {
                    salt = objAdmin.Salt;
                    string oldhash = objAdmin.Password;
                    bool flag = Operational.Encrypt.SaltedHash.Verify(salt, oldhash, oldPassword);
                    if (flag)
                    {
                        PWD = Operational.Encrypt.SaltedHash.ComputeHash(salt, Password);
                        objAdmin.Password = PWD;
                        _repositoryWrapper.Admin.Update(objAdmin);

                        _repositoryWrapper.EventLog.Info("Admin Change Password", "Success");
                        objresponse = new { data = true };
                    }
                }
            }
            return objresponse;
        }

        [HttpGet("unBlock/{AdminID}", Name = "unBlock")]
        [Authorize]
        public dynamic unBlock(int AdminID)
        {
            Admin objAdmin = _repositoryWrapper.Admin.FindByID(AdminID);
            dynamic objresponse;

            if (objAdmin != null)
            {
                objAdmin.login_fail_count = 0;
                objAdmin.access_status = 0;
                _repositoryWrapper.Admin.Update(objAdmin);
                objresponse = new { data = true };
                _repositoryWrapper.EventLog.Info("Admin Un-Block", "Success");
            }
            else
                objresponse = new { data = false };
            return objresponse;
        }

        [HttpPost("SaveImagePath", Name = "AdminSaveImagePath")]
        [Authorize]
        public dynamic SaveImagePath()
        {
            var objstr = HttpContext.Request.Form["objAdminInfo"];
            dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(objstr);
            int AdminID = obj.ID;
            string ext = obj.FileExt;
            string ImagePath = AdminID.ToString() + '.' + ext;

            Admin objAdmin = _repositoryWrapper.Admin.FindByID(AdminID);
            dynamic objresponse;

            if (objAdmin != null)
            {
                objAdmin.ImagePath = ImagePath;
                _repositoryWrapper.Admin.Update(objAdmin);
                objresponse = new { data = true };
            }
            else
                objresponse = new { data = false };
            return objresponse;
        }

        [HttpDelete("DeleteAdminSetup/{AdminID}", Name = "DeleteAdminSetup")]
        [Authorize]
        public dynamic DeleteAdminSetup(int AdminID)
        {
            bool retBool = false;

            //validateDelete
            var userresult = (_repositoryWrapper.EventLog.GetEventLogByUser(AdminID, "admin")).ToList();
            if (userresult.Count > 0)
            {
                retBool = false;
            }
            else
            {
                Admin objAdmin =_repositoryWrapper.Admin.FindByID(AdminID);
                if (objAdmin != null)
                {
                    _repositoryWrapper.Admin.Delete(objAdmin);
                    retBool = true;
                    _repositoryWrapper.EventLog.Delete("Delete Admin", objAdmin);
                }
            }
            dynamic objresponse = new { data = retBool };
            return objresponse;
        }
    }
}