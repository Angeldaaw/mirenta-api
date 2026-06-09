using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiRenta.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TentantController : ControllerBase
    {

        private readonly ITentantService _tenantService;

        public TentantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

    }
}
