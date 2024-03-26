using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TelegramBot.Domain;
using TelegramBot.Domain.Repositories;

namespace TelegramBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogRepository _catalogRepository;

        public CatalogController(ICatalogRepository catalogRepository) {
            _catalogRepository = catalogRepository;
        }
        [HttpPost]
        public async Task<IActionResult> AddCatalogItem(Product catalog)
        {
            var id = await _catalogRepository.Add(catalog);
            return Ok(id);
        }



    }
}
