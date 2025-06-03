using System.Security.Claims;
using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureLoginKit.Data;
using SecureLoginKit.Models;
using SecureLoginKit.Models.DTOs;
using SecureLoginKit.Services;
using SecureLoginKit.Services.Interfaces;

namespace SecureLoginKit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController (IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
