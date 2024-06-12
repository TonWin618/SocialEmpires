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
        public async Task<PageResult<Item>> GetItems(int pageIndex, int pageSize)
        {
            var (count, items) = await _configFileService.GetItemsAsync(pageIndex,pageSize);
            return Page(pageIndex, pageSize, count, items);
        }

        [HttpPost("changeName")]
        //[Authorize(Roles = "Admin")]
        public async Task<bool> ChangeName(ChangeNameRequest request)
        {
            return true;
        }
        public record ChangeNameRequest(string NewName);

        public PageResult<T> Page<T>(int pageIndex, int pageSize, int pageCount, IEnumerable<T>? data)
        {
            return new PageResult<T>(pageIndex, pageSize, pageCount, data?.Count()??0, data);
        }
        public record PageResult<T>(int PageIndex, int PageSize, int PageCount, int DataCount, IEnumerable<T>? Data);
    }
}
