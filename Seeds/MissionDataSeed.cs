﻿using AutoMapper;
using SocialEmpires.Dtos;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Seeds
{
    public class MissionDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;

        public MissionDataSeed(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        public void Initialize()
        {
            if (_appDbContext.Missions.Any())
            {
                return;
            }

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MissionDto, Mission>();
            }).CreateMapper();

            ConfigReadAndSaveUtil.ReadAndSave<Mission, MissionDto>("missions", _appDbContext, mapper);
        }
    }
}
