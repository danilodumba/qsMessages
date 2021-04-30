using System;
using System.Collections.Generic;
using qs.Messages.ApplicationServices.Core;
using qs.Messages.ApplicationServices.Models;

namespace qs.Messages.ApplicationServices.Services.Interfaces
{
    public interface ITemplateService: IApplicationService<TemplateModel, String>
    {
        IList<TemplateModel> Search(Guid? projectID, string id, string description);
    }
}