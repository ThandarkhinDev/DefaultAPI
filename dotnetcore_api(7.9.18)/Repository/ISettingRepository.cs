//using Entities.ExtendedModels;
using MFI;
using System;
using System.Collections.Generic;

namespace MFI.Repository
{
    public interface ISettingRepository
    {
       IEnumerable<Setting> GetPasswordValidation();
       IEnumerable<Setting> GetAllowLoginFailCount();
       IEnumerable<Setting> GetEmailSetting();

    }
       
}
