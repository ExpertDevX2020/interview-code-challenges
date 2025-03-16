using Microsoft.AspNetCore.Mvc;
using OneBeyondApi.DataAccess;
using OneBeyondApi.DTOs;
using OneBeyondApi.Model;
using System.Collections;

namespace OneBeyondApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogueController : ControllerBase
    {
        private readonly ILogger<CatalogueController> _logger;
        private readonly ICatalogueRepository _catalogueRepository;

        public CatalogueController(ILogger<CatalogueController> logger, ICatalogueRepository catalogueRepository)
        {
            _logger = logger;
            _catalogueRepository = catalogueRepository;   
        }

        [HttpGet]
        [Route("GetCatalogue")]
        public IEnumerable<BookStockDto> Get()
        {
            return _catalogueRepository.GetCatalogue();
        }

        [HttpPost]
        [Route("SearchCatalogue")]
        public IList<BookStockDto> Post(CatalogueSearch search)
        {
            return _catalogueRepository.SearchCatalogue(search);
        }
    }
}