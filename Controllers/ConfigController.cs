using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Models.Configs;
using SocialEmpires.Services;

namespace SocialEmpires.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/config")]
    public class ConfigController : ControllerBase
    {
        private readonly ConfigFileService _configFileService;

        public ConfigController(
            ConfigFileService configFileService,
            IMapper mapper)
        {
            _configFileService = configFileService;
        }

        #region Items
        [HttpGet("items")]
        //[Authorize(Roles = "Admin")]
        public Task<PageResult<Item>> GetItems(int pageIndex, int pageSize)
        {
            var (count, items) = _configFileService.GetItems(pageIndex, pageSize);
            return Task.FromResult(Page(pageIndex, pageSize, count, items));
        }

        [HttpGet("items/{id}")]
        public Task<Item?> GetItem(string id)
        {
            return Task.FromResult(_configFileService.GetItem(id));
        }

        [HttpPost("items/{id}/shelve")]
        //[Authorize(Roles = "Admin")]
        public async Task<bool> ShelveItem(string id)
        {
            var item = _configFileService.GetItem(id);
            if(item == null)
            {
                return false;
            }
            item.InStore = "1";
            await _configFileService.Save();
            return true;
        }

        [HttpPost("items/{id}/unshelve")]
        //[Authorize(Roles = "Admin")]
        public async Task<bool> UnshelveItem(string id)
        {
            var item = _configFileService.GetItem(id);
            if (item == null)
            {
                return false;
            }
            item.InStore = "0";
            await _configFileService.Save();
            return true;
        }

        [HttpPost("items/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<bool> UpdateItem(
            string id, 
            [FromForm] Item updatedItem,
            [FromServices] IMapper _mapper)
        {
            var item = _configFileService.Items.FirstOrDefault(_ => _.Id == id);
            _mapper.Map(updatedItem, item);
            await _configFileService.Save();
            return true;
        }
        #endregion

        public PageResult<T> Page<T>(int pageIndex, int pageSize, int pageCount, IEnumerable<T>? data)
        {
            return new PageResult<T>(pageIndex, pageSize, pageCount, data?.Count() ?? 0, data);
        }
        public record PageResult<T>(int PageIndex, int PageSize, int PageCount, int DataCount, IEnumerable<T>? Data);
    }
}
