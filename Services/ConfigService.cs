﻿using SocialEmpires.Infrastructure.MultiLanguage;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;
using SocialEmpires.Utils;
using System.Text.Json;

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
            var language = "en"; //TODO: 
            Items = _appDbContext.Items.WithLanguage(language).ToList();
            Missions = _appDbContext.Missions.WithLanguage(language).ToList();
            Levels = _appDbContext.Levels.WithLanguage(language).ToList();
            ExpansionPrices = _appDbContext.ExpansionPrices.WithLanguage(language).ToList();

            Globals = JsonDocument.Parse(_appDbContext.Chores.First().Globals).RootElement;
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
