using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WssCRM.DBModels;
using WssCRM.Register;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WssCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DBModels.ApplicationContext _db;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, DBModels.ApplicationContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return Ok();
        }
        [HttpGet("users")]
        [Authorize(Roles = "admin")]
        public IActionResult getUsers()
        {
            var users = _userManager.Users.Select(user => 
                new {id = user.Id, username = user.UserName, firstName = user.FirstName, lastName = user.LastName }
                ).ToList();
            return Ok(users);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserFromCLient model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.UserName, UserName = model.UserName, FirstName = model.FirstName, LastName = model.LastName };
                // добавляем пользователя
                
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    
                    return Ok();
                }
                else
                {
                    foreach (var error in result.Errors)
                    {

                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return BadRequest(ModelState);
                }
            }
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserFromCLient user)
        {
            if (ModelState.IsValid)
            {
                // установка куки
                var result =
                    await _signInManager.PasswordSignInAsync(user.UserName, user.Password, true, true);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(_db.Users.Where(u => u.UserName == user.UserName).First(), true);
                    //var curUser = await _userManager.GetUserAsync(this.User);
                    //ClaimsPrincipal currentUser = this.User;
                    //var skj = currentUser.Identity.Name;
                    //var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                    //var curUser = await _userManager.GetUserAsync(HttpContext.User);// .FindByNameAsync(currentUserName);
                    var curUser = _db.Users.Where(u => u.UserName == user.UserName).First();
                    // проверяем, принадлежит ли URL приложению
                    user.FirstName = curUser.FirstName;
                    user.LastName = curUser.LastName;
                    user.Password = "";
                    user.token = "fake-jwt-token";
                    return Ok(user);
                }
                else
                {
                    return Unauthorized();
                }
            }
            return Unauthorized();

        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}