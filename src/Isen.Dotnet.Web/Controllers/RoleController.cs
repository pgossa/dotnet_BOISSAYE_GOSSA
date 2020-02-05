using System.Linq;
using Isen.Dotnet.Library.Context;
using Isen.Dotnet.Library.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Isen.Dotnet.Web.Controllers
{
    public class RoleController : BaseController<Role>
    {
        public RoleController(
            ILogger<RoleController> logger,
            ApplicationDbContext context) : base(logger, context)
        {
        }

         protected override IQueryable<Role> BaseQuery() =>
            base.BaseQuery()
                .Include("rolepersons.person")
                .OrderBy(rp => rp.role);
    }
}