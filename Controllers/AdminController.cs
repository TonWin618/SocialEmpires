using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SocialEmpires.Hubs;
using SocialEmpires.Models;
using SocialEmpires.Services;

namespace SocialEmpires.Controllers
{
    [Authorize(Roles = "Admin")]
    public partial class AdminController : Controller
    {
        private readonly ConfigService _configFileService;
        private readonly PlayerSaveService _playerSaveService;
        private readonly IHubContext<BulletinHub> _bulletinHubContext;
        private readonly AppDbContext _appDbContext;

        public AdminController(
            ConfigService configFileService,
            PlayerSaveService playerSaveService,
            IHubContext<BulletinHub> bulletinHubContext,
            AppDbContext appDbContext)
        {
            _configFileService = configFileService;
            _playerSaveService = playerSaveService;
            _bulletinHubContext = bulletinHubContext;
            _appDbContext = appDbContext;
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

        private string UserId => HttpContext.User.Identity.Name;
    }
}
