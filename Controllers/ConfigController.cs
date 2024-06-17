﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialEmpires.Models.Configs;
using SocialEmpires.Services;
using System.Text.Json.Serialization;

namespace SocialEmpires.Controllers
{
    [Route("api/admin/config")]
    [ApiController]
    public class ConfigController:ControllerBase
    {
        private readonly ConfigFileService _configFileService;
        private readonly IMapper _mapper;

        public ConfigController(
            ConfigFileService configFileService,
            IMapper mapper)
        {
            _configFileService = configFileService;
            _mapper = mapper;
        }

        #region Items
        [HttpGet("items")]
        //[Authorize(Roles = "Admin")]
        public async Task<PageResult<Item>> GetItems(int pageIndex, int pageSize)
        {
            var (count, items) = _configFileService.GetItems(pageIndex, pageSize);
            return Page(pageIndex, pageSize, count, items);
        }

        [HttpGet("items/{id}")]
        public async Task<Item?> GetItem(string id)
        {
            return _configFileService.GetItem(id);
        }

        [HttpPost("items/{id}/shelve")]
        //[Authorize(Roles = "Admin")]
        public async Task<bool> ShelveItem(string id)
        {
            var item = _configFileService.GetItem(id);
            item.InStore = "1";
            await _configFileService.Save();
            return true;
        }

        [HttpPost("items/{id}/unshelve")]
        //[Authorize(Roles = "Admin")]
        public async Task<bool> UnshelveItem(string id)
        {
            var item = _configFileService.GetItem(id);
            item.InStore = "0";
            await _configFileService.Save();
            return true;
        }

        [HttpPost("items/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<bool> UpdateItem(string id, [FromForm]Item updatedItem)
        {
            var item = _configFileService.Items.FirstOrDefault(_ => _.Id == id);
            _mapper.Map(updatedItem, item);
            await _configFileService.Save();
            return true;
        }
        #endregion

        public PageResult<T> Page<T>(int pageIndex, int pageSize, int pageCount, IEnumerable<T>? data)
        {
            return new PageResult<T>(pageIndex, pageSize, pageCount, data?.Count()??0, data);
        }
        public record PageResult<T>(int PageIndex, int PageSize, int PageCount, int DataCount, IEnumerable<T>? Data);
    }
}
