using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Services;

namespace SocialEmpires.Controllers
{
    [Authorize(Roles = "Admin")]
    public partial class AdminController : Controller
    {
        private readonly ConfigFileService _configFileService;
        private readonly PlayerSaveService _playerSaveService;

        public AdminController(
            ConfigFileService configFileService,
            PlayerSaveService playerSaveService)
        {
            _configFileService = configFileService;
            _playerSaveService = playerSaveService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public PageResult<T> Page<T>(int pageIndex, int pageSize, int pageCount, IEnumerable<T>? data)
        {
            return new PageResult<T>(pageIndex, pageSize, pageCount, data?.Count() ?? 0, data);
        }
        public record PageResult<T>(int PageIndex, int PageSize, int PageCount, int DataCount, IEnumerable<T>? Data);
    }
}
