using AutoMapper;
using SocialEmpires.Dtos;
using SocialEmpires.Models.Configs;
using SocialEmpires.Models;
using AngleSharp.Dom;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json.Nodes;
using System.Text.Json;
using System;
using SocialEmpires.Infrastructure.MultiLanguage;
using Microsoft.IdentityModel.Tokens;

namespace SocialEmpires.Seeds
{
    public class TournamentTypeDataSeed:IDataSeed
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public TournamentTypeDataSeed(AppDbContext appContext, IMapper mapper)
        {
            _appDbContext = appContext;
            _mapper = mapper;
        }

        public void Initialize()
        {
            if (_appDbContext.TournamentTypes.Any())
            {
                return;
            }
            var key = "tournament_type";

            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
            }.WithLanguage("en");

            JsonNode config;
            using (var stream = File.OpenRead("./Seeds/game_config_en.json"))
            {
                config = JsonNode.Parse(stream) ?? throw new InvalidOperationException();
            }

            var entities = new List<TournamentType>();
            var objs = config[key].AsObject();

            foreach (var obj in objs)
            {
                var weeklyOpponentEntities = new List<TournamentOpponent>();
                var weeklyOpponent = obj.Value["weekly_opponent"];
                if (weeklyOpponent != null)
                {
                    foreach (var opponent in weeklyOpponent.AsObject())
                    {
                        var opponentDto = opponent.Value.Deserialize<TournamentOpponentDto>(jsonSerializerOptions);
                        opponentDto.Id = opponent.Key;
                        weeklyOpponentEntities.Add(_mapper.Map<TournamentOpponentDto, TournamentOpponent>(opponentDto));
                    }
                    obj.Value["weekly_opponent"].ReplaceWith((TournamentOpponentDto)null);
                }

                var dto = obj.Value.Deserialize<TournamentTypeDto>(jsonSerializerOptions);
                var entity = _mapper.Map<TournamentTypeDto, TournamentType>(dto);

                if (!weeklyOpponentEntities.IsNullOrEmpty())
                {
                    entity.WeeklyOpponent = weeklyOpponentEntities;
                }

                entities.Add(entity);
            }

            foreach (var entity in entities)
            {
                _appDbContext.Add(entity);
            }
            //appDbContext.AddRange(entities);
            _appDbContext.SaveChanges();
        }
    }
}
