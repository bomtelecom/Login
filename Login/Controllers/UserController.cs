using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Login.Models;
using Login.Services;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Login.Entities;
using AutoMapper;
using Login.Helpers;
using Microsoft.Extensions.Options;

namespace Login.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        public UserController(
            IUserService userService,
            IOptions<AppSettings> appSettings,
            IMapper mapper)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }
        [HttpPost("authenticate")]
        public IActionResult Autentificate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model.Username,model.Password);

            if (response == null)
                return BadRequest(new { message = "User or Password is incorrect" });
            return Ok(response);
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            // map model to entity
            var user = _mapper.Map<User>(model);

            try
            {
                // create user
                _userService.Create(user,model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
        /*
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            try
            {
                // create user
                //_userService.Create(user, model.Password);
                return Ok();
            }
            catch (Exception)
            {
                // return error message if there was an exception
                return BadRequest(new { message = "Unable to create user" });
            }
            /*
            using (var serviceScope = _appBuilder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<UserContext>();
                try
                {
                    var appUser = new User()
                    {
                        UserName = model.UserName,
                        Password = model.Password,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                    };
                    var result = context.Users.Add(appUser);
                    return Ok(result);
                }
	            catch (Exception)
	            {

		            throw;
	            }
            }
           
        }
        */
    }
}
