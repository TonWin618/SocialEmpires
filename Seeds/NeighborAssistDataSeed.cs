﻿using AutoMapper;
using SocialEmpires.Dtos;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Seeds
{
    public class NeighborAssistDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;

        public NeighborAssistDataSeed(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        public void Initialize()
        {
            if (_appDbContext.NeighborAssists.Any())
            {
                return;
            }

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NeighborAssistDto, NeighborAssist>();
            }).CreateMapper();

            ConfigReadAndSaveUtil.ReadAndSave<NeighborAssist, NeighborAssistDto>("neighbor_assists", _appDbContext, mapper);
        }
    }
}
