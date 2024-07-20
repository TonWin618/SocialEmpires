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
    }
}
