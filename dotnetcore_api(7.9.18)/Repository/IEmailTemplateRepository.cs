//using Entities.ExtendedModels;
using System;
using System.Collections.Generic;

namespace MFI.Repository
{
    public interface IEmailTemplateRepository
    {
        IEnumerable<EmailTemplate> GetEmailTemplate(string templateName);
    }
}
