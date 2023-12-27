using System.Collections.Generic;
using System.Linq;
using System;
using Repository;

namespace MFI.Repository
{
    public class EmailTemplateRepository : RepositoryBase<EmailTemplate>, IEmailTemplateRepository
    {
        public EmailTemplateRepository(AppDb repositoryContext)
            : base(repositoryContext)
        {
        }
        public IEnumerable<EmailTemplate> GetEmailTemplate(string templateName) {
            return FindByCondition(x => x.template_name == templateName);
        }
    }
}
