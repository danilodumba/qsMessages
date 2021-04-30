using System;
using System.Collections.Generic;
using qs.Messages.ApplicationServices.Core;
using qs.Messages.ApplicationServices.Models;

namespace qs.Messages.ApplicationServices.Services.Interfaces
{
    public interface IProjectService: IApplicationService<ProjectModel, Guid>
    {
        IList<ProjectModel> ListByName(string name);
    }
}