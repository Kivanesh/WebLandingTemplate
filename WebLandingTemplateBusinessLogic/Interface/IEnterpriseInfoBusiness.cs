﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLandingTemplateDomainModel.Models;

namespace WebLandingTemplateBusinessLogic.Interface
{
    public interface IEnterpriseInfoBusiness
    {
        EnterpriseInfoDto GetEnterpriseInfo();
        string UpdateItem(EnterpriseInfoDto ObjModel);
    }
}
