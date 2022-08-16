namespace WebService.API.ModelControllers {
    public class CountriesController : ApiControllerBase {
        public CountriesController(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, IMapper mapper, LfidContext lfidContext, ILogger<ApiControllerBase> logger)
            : base(actionDescriptorCollectionProvider, mapper, lfidContext, logger)
        { }

        [HttpGet("shadow", Name = nameof(GetShadowCountries))]
        [HttpHead("shadow")]
        public async Task<ActionResult<IEnumerable<CountryModel>>> GetShadowCountries() {
            var res = await lfidContext.Country.ToListAsync();
            if (!res.Any()) throw new DataNotFoundException();
            return Ok(res.OrderBy(c => c.CustareaCode)
                        .ThenBy(c => c.IcaoCode)
                        .GroupBy(c => c.IcaoCode, (key, c) => c.FirstOrDefault())
                        .Select(c => Mapper.Map<CountryModel>(c)));
        }
    }
}