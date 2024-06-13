using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialEmpires.Models.Configs;
using SocialEmpires.Services;

namespace SocialEmpires.Controllers
{
    [Route("api/admin/config")]
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
        public async Task<PageResult<Item>> GetItems(int pageIndex, int pageSize)
        {
            var (count, items) = await _configFileService.GetItemsAsync(pageIndex,pageSize);
            return Page(pageIndex, pageSize, count, items);
        }

        [HttpGet("items/{id}")]
        public async Task<Item?> GetItem(string id)
        {
            return await _configFileService.GetItemAsync(id);
        }

        [HttpPost("items/{id}/shelve")]
        //[Authorize(Roles = "Admin")]
        public async Task<bool> ShelveItem(string id)
        {
            var item = await _configFileService.GetItemAsync(id);
            item.InStore = "1";
            await _configFileService.Save();
            return true;
        }

        [HttpPost("items/{id}/unshelve")]
        //[Authorize(Roles = "Admin")]
        public async Task<bool> UnshelveItem(string id)
        {
            var item = await _configFileService.GetItemAsync(id);
            item.InStore = "0";
            await _configFileService.Save();
            return true;
        }

        [HttpPut("items/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<bool> UnshelveItem(string id, [FromForm]Item updatedItem)
        {
            var item = await _configFileService.GetItemAsync(id);
            item = updatedItem;
            await _configFileService.Save();
            return true;
        }


        public PageResult<T> Page<T>(int pageIndex, int pageSize, int pageCount, IEnumerable<T>? data)
        {
            return new PageResult<T>(pageIndex, pageSize, pageCount, data?.Count()??0, data);
        }
        public record PageResult<T>(int PageIndex, int PageSize, int PageCount, int DataCount, IEnumerable<T>? Data);
    }
}
