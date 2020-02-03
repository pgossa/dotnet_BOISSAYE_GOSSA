using System;
using System.Linq;
using Isen.Dotnet.Library.Context;
using Isen.Dotnet.Library.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Isen.Dotnet.Web.Controllers
{
    public class ServiceController : BaseController<Service>
    {
        public ServiceController(
            ILogger<ServiceController> logger,
            ApplicationDbContext context) : base(logger, context)
        {
        }
    }
}