using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;
using SocialEmpires.Models.Options;
using SocialEmpires.Utils;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SocialEmpires.Services
{
    public class ConfigService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<ConfigService> _logger;

        public List<Item> Items { get; private set; }
        public List<Mission> Missions { get; private set; }
        public List<Level> Levels { get; private set; }
        public List<ExpansionPrice> ExpansionPrices { get; private set; }
        public JsonElement Globals { get; private set; }

        public ConfigService(
            AppDbContext appDbContext,
            ILogger<ConfigService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            Load();
        }

        public void Load()
        {
            Items = _appDbContext.Items.WithLanguage("en").ToList();
        }

        public Item? GetItem(int id)
        {
            return GetItem(id.ToString());
        }

        public Item? GetItem(string id)
        {
            return Items?.FirstOrDefault(_ => _.Id == int.Parse(id));
        }

        public (int pageCount, IEnumerable<Item>? items) GetItems(int pageIndex, int pageSize)
        {
            return PageHelper.Page(pageIndex, pageSize, Items);
        }
    }
}
