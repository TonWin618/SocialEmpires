using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Controllers
{
    public partial class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Items(int pageIndex = 1, int pageSize = 20)
        {
            ViewData["PageIndex"] = pageIndex;
            ViewData["PageSize"] = pageSize;
            (ViewData["PageCount"], ViewData["PageData"]) = _configService.GetItems(pageIndex, pageSize);
            return View();
        }

        [HttpGet]
        public IActionResult Item(int id)
        {
            var item = _configService.GetItem(id);
            ViewData["Item"] = item;
            if (item == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ShelveItem(string id, string pageIndex)
        {
            var item = _configService.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }
            item.InStore = true;
            //await _configFileService.Save();
            return Redirect(Request.Headers.Referer);
        }

        [HttpPost]
        public async Task<IActionResult> UnshelveItem(string id, string pageIndex)
        {
            var item = _configService.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }
            item.InStore = false;
            //await _configFileService.Save();
            return Redirect(Request.Headers.Referer);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateItem(
            [FromForm] Item updatedItem,
            [FromServices] IMapper _mapper)
        {
            var item = _configService.Items.FirstOrDefault(_ => _.Id == updatedItem.Id);
            _mapper.Map(updatedItem, item);
            //await _configFileService.Save();
            return Redirect(Request.Headers.Referer);
        }
    }
}
