using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Test.Api.Controllers
{
    public class DepartmentController : BaseController<Department>
    {
        public DepartmentController(IBaseService<Department> baseService) : base(baseService)
        {
          
        }
    }
}
