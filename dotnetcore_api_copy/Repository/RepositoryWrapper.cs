using MFI;
using MFI.Repository;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private AppDb _repoContext;
        private IAdminRepository _Admin;
        private IAdminLevelRepository _AdminLevel;

        private IStateRepository _State;
        private ISettingRepository _Setting;
        private IEventLogRepository _EventLog;
        private IEmailTemplateRepository _EmailTemplate;
        private IAdminMenuUrlRepository _AdminMenuUrl;
        private IAdminlevelmenuRepository _Adminlevelmenu;
        private IAdminMenuRepository _AdminMenu;
        private ISampleRepository _Sample;
        private IIncomeRepository _Income;
        private IExpenseRepository _Expense;

        public ISampleRepository Sample
        {
            get
            {
                if (_Sample == null)
                {
                    _Sample = new SampleRepository(_repoContext);
                }

                return _Sample;
            }
        }
        public IIncomeRepository Income
        {
            get
            {
                if (_Income == null)
                {
                    _Income = new IncomeRepository(_repoContext);
                }

                return _Income;
            }
        }
        public IExpenseRepository Expense
        {
            get
            {
                if (_Expense == null)
                {
                    _Expense = new ExpenseRepository(_repoContext);
                }

                return _Expense;
            }
        }
        public IAdminMenuRepository AdminMenu
        {
            get
            {
                if (_AdminMenu == null)
                {
                    _AdminMenu = new AdminMenuRepository(_repoContext);
                }

                return _AdminMenu;
            }
        }
        public IAdminlevelmenuRepository Adminlevelmenu
        {
            get
            {
                if (_Adminlevelmenu == null)
                {
                    _Adminlevelmenu = new AdminlevelmenuRepository(_repoContext);
                }

                return _Adminlevelmenu;
            }
        }
        public IAdminMenuUrlRepository AdminMenuUrl
        {
            get
            {
                if (_AdminMenuUrl == null)
                {
                    _AdminMenuUrl= new AdminMenuUrlRepository(_repoContext);
                }

                return _AdminMenuUrl;
            }
        }
        public IEmailTemplateRepository EmailTemplate
        {
            get
            {
                if (_EmailTemplate == null)
                {
                    _EmailTemplate = new EmailTemplateRepository(_repoContext);
                }

                return _EmailTemplate;
            }
        }
        public ISettingRepository Setting
        {
            get
            {
                if (_Setting == null)
                {
                    _Setting = new SettingRepository(_repoContext);
                }

                return _Setting;
            }
        }

        public IAdminRepository Admin
        {
            get
            {
                if (_Admin == null)
                {
                    _Admin = new AdminRepository(_repoContext);
                }

                return _Admin;
            }
        }

        public IAdminLevelRepository AdminLevel
        {
            get
            {
                if (_AdminLevel == null)
                {
                    _AdminLevel = new AdminLevelRepository(_repoContext);
                }

                return _AdminLevel;
            }
        }

        public IStateRepository State
        {
            get
            {
                if (_State == null)
                {
                    _State = new StateRepository(_repoContext);
                }

                return _State;
            }
        }

        public IEventLogRepository EventLog
        {
            get
            {
                if (_EventLog == null)
                {
                    _EventLog = new EventLogRepository(_repoContext);
                }

                return _EventLog;
            }
        }
     //   private IUserLoginActivityRepository _UserLoginActivity;
     //   private IAdminRepository _Admin;
     //   private IAdminLevelRepository _AdminLevel;
     //   private IAdminMenuUrlRepository _AdminMenuUrl;
     //   private IAdminlevelmenuRepository _Adminlevelmenu;
     //   private IEventLogRepository _EventLog;
     //   private ISettingRepository _Setting;
     //   private ILenderRepository _Lender;
      //  private IEmailTemplateRepository _EmailTemplate;
/* 
		public IEmailTemplateRepository EmailTemplate
        {
            get
            {
                if (_EmailTemplate == null)
                {
                    _EmailTemplate = new EmailTemplateRepository(_repoContext);
                }

                return _EmailTemplate;
            }
        }
		
		public ILenderRepository Lender
        {
            get
            {
                if (_Lender == null)
                {
                    _Lender = new LenderRepository(_repoContext);
                }

                return _Lender;
            }
        }
		
		public IEventLogRepository EventLog
        {
            get
            {
                if (_EventLog == null)
                {
                    _EventLog = new EventLogRepository(_repoContext);
                }

                return _EventLog;
            }
        } */
		
		// public IAdminlevelmenuRepository Adminlevelmenu
        // {
        //     get
        //     {
        //         if (_Adminlevelmenu == null)
        //         {
        //             _Adminlevelmenu = new AdminlevelmenuRepository(_repoContext);
        //         }

        //         return _Adminlevelmenu;
        //     }
        // }
		
		// public IAdminRepository Admin
        // {
        //     get
        //     {
        //         if (_Admin == null)
        //         {
        //             _Admin = new AdminRepository(_repoContext);
        //         }

        //         return _Admin;
        //     }
        // }
		
		// public IAdminMenuUrlRepository AdminMenuUrl
        // {
        //     get
        //     {
        //         if (_AdminMenuUrl == null)
        //         {
        //             _AdminMenuUrl= new AdminMenuUrlRepository(_repoContext);
        //         }

        //         return _AdminMenuUrl;
        //     }
        // }
		
		// public IAdminLevelRepository AdminLevel
        // {
        //     get
        //     {
        //         if (_AdminLevel == null)
        //         {
        //             _AdminLevel = new AdminLevelRepository(_repoContext);
        //         }

        //         return _AdminLevel;
        //     }
        // }

        // public IUserLoginActivityRepository UserLoginActivity
        // {
        //     get
        //     {
        //         if (_UserLoginActivity == null)
        //         {
        //             _UserLoginActivity = new UserLoginActivityRepository(_repoContext);
        //         }

        //         return _UserLoginActivity;
        //     }
        // }

        // public ISettingRepository Setting
        // {
        //     get
        //     {
        //         if (_Setting == null)
        //         {
        //             _Setting = new SettingRepository(_repoContext);
        //         }

        //         return _Setting;
        //     }
        // }



        public RepositoryWrapper(AppDb repositoryContext)
        {
            _repoContext = repositoryContext;
        }
    }
}
