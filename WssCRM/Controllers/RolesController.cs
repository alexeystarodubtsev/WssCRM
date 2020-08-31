using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WssCRM.DBModels;
using WssCRM.Models;
using WssCRM.Register;

namespace WssCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class RolesController : ControllerBase
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<User> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(Role rolefromclient)
        {
            
            if (!string.IsNullOrEmpty(rolefromclient.Name))
            {
                IdentityRole role = new IdentityRole { Name = rolefromclient.Name, NormalizedName = rolefromclient.NormalizedName };
                IdentityResult result = await _roleManager.CreateAsync(role);
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
                    return BadRequest(result.Errors.First().Description);
                }
            }
            else
                return BadRequest("Пустое имя роли");
        }
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return Ok();
        }
        [HttpGet("edit/{userId}")]
        public async Task<IActionResult> Edit(string userId)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                UserForChangeRole userToClient = new UserForChangeRole();
                userToClient.UserId = user.Id;
                userToClient.UserName = user.Email;
                userToClient.roles = _roleManager.Roles.Where(r => userRoles.Any(ur => ur == r.Name)).ToList();
                userToClient.allroles = allRoles;
                
                return Ok(userToClient);
            }

            return NotFound();
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit(string userId, List<Role> roles)
        {
            var roleNames = roles.Select(r => r.Name);
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roleNames.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roleNames);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return Ok();
            }

            return NotFound();
        }
    }
}