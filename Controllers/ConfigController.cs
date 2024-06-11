using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Models;
using SocialEmpires.Services;

namespace SocialEmpires.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ConfigController:ControllerBase
    {
        private readonly ConfigFileService _configFileService;
        public ConfigController(
            ConfigFileService configFileService)
        {
            _configFileService = configFileService;
        }

        [HttpGet("items")]
        //[Authorize(Roles = "Admin")]
        public async Task<IEnumerable<Item>> GetItems()
        {
            var items = await _configFileService.GetAllItemsAsync("zh");
            return items;
        }
    }
}
