using System;
using System.Collections.Generic;
using System.Text;

namespace MFI.Repository
{
    public interface IRepositoryWrapper
    {
      IAdminRepository Admin { get; }
      IAdminLevelRepository AdminLevel { get; }
      IStateRepository State { get; }
      IEventLogRepository EventLog { get; }
      ISettingRepository Setting { get; }
      IEmailTemplateRepository EmailTemplate { get; }
      IAdminMenuUrlRepository AdminMenuUrl { get; }
      IAdminlevelmenuRepository Adminlevelmenu { get; }
      
      IAdminMenuRepository AdminMenu { get; }
      ISampleRepository Sample { get; }
      IIncomeRepository Income { get; }
      IExpenseRepository Expense { get; }


    }
}
