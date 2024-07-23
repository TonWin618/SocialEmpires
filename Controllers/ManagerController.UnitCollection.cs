using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialEmpires.Models.Configs;
using SocialEmpires.Utils;
using System.Globalization;

namespace SocialEmpires.Controllers
{
    public partial class ManagerController
    {
        public IActionResult UnitCollections(int pageIndex = 1, int pageSize = 20)
        {
            ViewData["PageIndex"] = pageIndex;
            ViewData["PageSize"] = pageSize;
            IEnumerable<UnitsCollectionsCategory> unitCollections;
            (ViewData["PageCount"], unitCollections) = PageHelper.Page(pageIndex, pageSize, _configService.UnitsCollectionsCategories);
            var unitCollectionDtos = unitCollections.Select(collection => new UnitCollectionDto
            (
                Id: collection.Id,
                CategoryLangId: collection.CategoryLangId,
                Position: collection.Position,
                Units: collection.Units
                .Select(unit => _configService.GetItem(unit).Name.Get(CultureInfo.CurrentCulture.Name))
                .ToList(),
                Rewards: _configService.GetItem(collection.Rewards).Name.Get(CultureInfo.CurrentCulture.Name),
                Cost: collection.Cost,
                Costs: collection.Costs
            ));
            ViewData["PageData"] = unitCollectionDtos;
            return View();
        }
        public record UnitCollectionDto(
            int Id, int CategoryLangId, int Position, List<string> Units, 
            string Rewards, int Cost, List<int>? Costs);

        [HttpPost]
        [Authorize(Roles = "UnitCollectionManager")]
        public async Task<IActionResult> AddUnitCollection(string units, string costs, int rewards)
        {
            var costNums = costs.Split(',').Select(int.Parse);
            var cost = costNums.First();
            if(costNums.All(_ => _.Equals(costNums.First())))
            {
                costNums = null;
            }

            var category = new UnitsCollectionsCategory()
            {
                CategoryId = _appDbContext.UnitsCollectionsCategories.Max(c => c.CategoryId) + 1,
                Position = _appDbContext.UnitsCollectionsCategories.Max(c => c.Position) + 5,
                CategoryLangId = _appDbContext.UnitsCollectionsCategories.Max(c => c.CategoryLangId) + 1,
                Units = units.Split(',').Select(int.Parse).ToList(),
                Cost = cost,
                Costs = costNums?.ToList(),
                Rewards = rewards
            };

            await _appDbContext.UnitsCollectionsCategories.AddAsync(category);
            return this.Redirect();
        }

        [HttpPost]
        [Authorize(Roles = "UnitCollectionManager")]
        public async Task<IActionResult> DeleteUnitCollection(int Id)
        {
            var collection = _appDbContext.UnitsCollectionsCategories.Find(Id);
            if(collection != null)
            {
                _appDbContext.UnitsCollectionsCategories.Remove(collection);
            }
            return this.Redirect();
        }
    }
}
