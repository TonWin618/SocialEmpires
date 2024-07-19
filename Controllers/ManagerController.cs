﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SocialEmpires.Infrastructures.NotificationHub;
using SocialEmpires.Models;
using SocialEmpires.Services;

namespace SocialEmpires.Controllers
{
    [Authorize(Roles = "Manager")]
    public partial class ManagerController : Controller
    {
        private readonly ConfigService _configService;
        private readonly PlayerSaveService _playerSaveService;
        private readonly IHubContext<NotificationHub> _bulletinHubContext;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ManagerController(
            ConfigService configFileService,
            PlayerSaveService playerSaveService,
            IHubContext<NotificationHub> bulletinHubContext,
            AppDbContext appDbContext,
            IMapper mapper,
            IMediator mediator)
        {
            _configService = configFileService;
            _playerSaveService = playerSaveService;
            _bulletinHubContext = bulletinHubContext;
            _appDbContext = appDbContext;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.UseAdmin = false;
            ViewBag.UseManager = true;
            return View();
        }

        private string UserId => HttpContext.User.Identity!.Name!;
    }
}
