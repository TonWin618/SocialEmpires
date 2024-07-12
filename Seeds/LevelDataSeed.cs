﻿using AutoMapper;
using SocialEmpires.Dtos;
using SocialEmpires.Models;
using SocialEmpires.Models.Configs;

namespace SocialEmpires.Seeds
{
    public class LevelDataSeed : IDataSeed
    {
        private readonly AppDbContext _appDbContext;
        public LevelDataSeed(AppDbContext appContext)
        {
            _appDbContext = appContext;
        }

        public void Initialize()
        {
            if (_appDbContext.Levels.Any())
            {
                return;
            }

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LevelDto, Level>()
                .ForMember(
                    dest => dest.ToLevel,
                    opt => { opt.MapFrom(src => MappingCounter.Count + 1); })
                .AfterMap((src, dest) => MappingCounter.Increment());
            }).CreateMapper();

            ConfigReadAndSaveUtil.ReadAndSave<Level, LevelDto>("levels", _appDbContext, mapper);
        }
    }



    internal static class MappingCounter
    {
        private static int _count = 0;

        public static int Count => _count;

        public static void Increment() => _count++;
    }
}
