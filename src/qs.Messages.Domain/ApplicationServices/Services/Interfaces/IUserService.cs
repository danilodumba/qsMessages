using System;
using System.Collections.Generic;
using qs.Messages.ApplicationServices.Core;
using qs.Messages.ApplicationServices.Models;

namespace qs.Messages.ApplicationServices.Services.Interfaces
{
    public interface IUserService: IApplicationService<UserModel, Guid>
    {
        IList<UserModel> ListByName(string name);
    }
}