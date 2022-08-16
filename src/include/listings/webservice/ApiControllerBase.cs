namespace WebService.API {
    [ApiController]
    [Produces("application/json")]
    [Route("api/controller")]
    [Authorize(AuthenticationSchemes = "SchemeReadAccess, SchemeWriteAccess",
        Policy = "CustomPolicy")]
    public class ApiControllerBase : Controller {
        private readonly IReadOnlyList<ActionDescriptor> _routes;
        protected readonly LfidContext lfidContext;
        protected readonly ILogger<ApiControllerBase> Logger;
        protected readonly IMapper Mapper;
        public ApiControllerBase(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider,
            IMapper mapper, LfidContext lfidContext, ILogger<ApiControllerBase> logger) {
            _routes = actionDescriptorCollectionProvider.ActionDescriptors.Items;
            Mapper = mapper;
            LfidContext = lfidContext;
            Logger = logger;
        }
    }
}