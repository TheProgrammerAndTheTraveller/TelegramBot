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
        private readonly IProductRepository _catalogRepository;

        public CatalogController(IProductRepository catalogRepository) {
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
