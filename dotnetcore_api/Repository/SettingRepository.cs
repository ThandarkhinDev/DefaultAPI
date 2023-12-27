using System.Collections.Generic;
using System.Linq;
using System;
using Repository;

namespace MFI.Repository
{
    public class SettingRepository: RepositoryBase<Setting>, ISettingRepository
    {
        public SettingRepository(AppDb repositoryContext):base(repositoryContext)
        {
           
        }
        public IEnumerable<Setting> GetPasswordValidation() {
            return FindByCondition(x => x.Name == "Password Validation");
        }

        public IEnumerable<Setting> GetAllowLoginFailCount() {
            return FindByCondition(x => x.Name == "Allow Login Failure Count");
        }
        
        public IEnumerable<Setting> GetEmailSetting() {
            return FindByCondition(x => x.Name == "SMTP" || x.Name == "Email"
                                    || x.Name == "Email Password" || x.Name == "Server Port"  )
                                .OrderBy(x => x.SettingID);
            
        }
    }
}
