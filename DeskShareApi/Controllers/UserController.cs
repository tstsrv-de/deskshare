using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeskShareApi.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace DeskShareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly ILogger<UserController> _logger;
        public UserController(UserManager<UserIdentity> userManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger;
            LogInformation("open 'HomeController'");
        }

        #region Logging

        private void LogInformation(string message)
        {
            _logger.LogInformation($"{DateTime.Now} - Information:{Environment.NewLine}{message}");
        }
        private void LogWarning(string message)
        {
            _logger.LogWarning($"{DateTime.Now} - Warning:{Environment.NewLine}{message}");
        }
        private void LogError(string message)
        {
            _logger.LogError($"{DateTime.Now} - Error:{Environment.NewLine}{message}");
        }

        #endregion

        [HttpGet]
        [Route("perm")]
        [Authorize]
        public async Task<IActionResult> GetPermStatus(string uid)
        {
            var user = await _userManager.FindByIdAsync(uid);
            if (user._Perm)
            {
                LogInformation($"GetPermStatus user '{uid}' got permission");
                return Ok();
            }

            LogWarning($"GetPermStatus user '{uid} denied'");
            return Unauthorized();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password)) {
                _logger.LogWarning($"+++\nLogin denied\n+++");
                return Unauthorized();
            }

            var authClaims = CreateClaim(model);
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Bf]R86kM+buEB3'K"));
            var token = CreateToken(authClaims, authSigningKey);

            _logger.LogInformation("login Ok");
            _logger.LogInformation($"Token: {token}");
            return Ok(new
            {
                user=user.Id,
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        private static JwtSecurityToken CreateToken(IEnumerable<Claim> authClaims,SecurityKey authSigningKey)
        {
            return new JwtSecurityToken(
                expires: DateTime.Now.AddDays(7),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }

        private static IEnumerable<Claim> CreateClaim(LoginModel model)
        {
            return new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, model.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
        }

    }
}
