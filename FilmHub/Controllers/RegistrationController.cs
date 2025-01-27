﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Linq;
using Database;
using Database.DbModels;
using Database.Film;
using Database.User;
using Microsoft.AspNetCore.Mvc;
using FilmHub.Models;
using FilmHub.Services.Registration;
using FilmHub.Services.User;


namespace FilmHub.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService _registrationService;
        private readonly IUserService _userService;

        public RegistrationController(IRegistrationService registrationService, IUserService userService)
        {
                _registrationService = registrationService;
                _userService = userService;
        }

        [HttpGet]
        public IActionResult Index(string email, string password, string firstName, string lastName, string country, string dateOfBirth)
        {
            var model = new UserViewModel()
            {
                Email = email,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Country = country,
                DateOfBirth = dateOfBirth,
                Favourite = new List<Film>()
            };
            return View(model);
        }
        

        [HttpPost]
        public IActionResult Index(UserViewModel model)
        {
            var newUser = new User()
            {
                FirstName = model.FirstName,
                Email = model.Email,
                Password = model.Password,
                LastName = model.LastName,
                Country = model.Country,
                DateOfBirth = model.DateOfBirth,
                Favourite = new List<Film>()
            };
            var userId = _userService.Add(newUser);
            HttpContext.Response.Cookies.Append("currentUserId",userId.ToString());
            IRegistrationService.isLogged = true;
            IRegistrationService.currentUserId = userId;
            return RedirectToAction("ShowPersonalPage", "User");
        }
        

        [HttpGet]
        public IActionResult LogIn(string email, string password)
        {
            var count = _userService.Count();
            if (count == 0)
            {
                return RedirectToAction("Index", "Registration");
            }

            var logInModel = new UserViewModel()
            {
                Email = email,
                Password = password
            };
            return View(logInModel);
        }

        [HttpPost]
        public IActionResult LogIn(UserViewModel logInModel)
        {
            var currUser = new User()
            {
                Email = logInModel.Email,
                Password = logInModel.Password,
            };
            bool correct = _userService.Find(currUser);
            if (correct)
            {
                HttpContext.Response.Cookies.Append("currentUserId",
                    _userService.CurrentUser_Id(currUser).ToString());
                IRegistrationService.isLogged = true;
                IRegistrationService.currentUserId = _userService.CurrentUser_Id(currUser);
                return RedirectToAction("ShowPersonalPage", "User");
            }
            else
            {
                return RedirectToAction("LogIn", "Registration");
            }
        }
    }
}