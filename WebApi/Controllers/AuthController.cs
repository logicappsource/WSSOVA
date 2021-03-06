﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DatabaseService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;

using WebApi.WebServiceToken.Services;

namespace WebApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly IDataService _dataService;
    private IConfiguration _configuration;

    public AuthController(IDataService dataService, IConfiguration configuration)
    {
      _dataService = dataService;
      _configuration = configuration;
    }


    [HttpPost("create/user")]
    public ActionResult CreateUser([FromBody] UserForCreationDto dto)
    {
      if (_dataService.GetUser(dto.UserName) != null)
      {
        return BadRequest();
      }

      int.TryParse(
          _configuration.GetSection("Auth:PwdSize").Value,
          out var size);

      if (size == 0)
      {
        throw new ArgumentException();
      }

      var salt = PasswordService.GenerateSalt(size);

      var pwd = PasswordService.HashPassword(dto.Password, salt, size);

      _dataService.CreateUser(dto.UserName, pwd, salt);

      return CreatedAtRoute(null, dto.UserName);
    }

    [HttpPost("token")]
    public ActionResult Login([FromBody] UserForLoginDto dto)
    {
      var user = _dataService.GetUser(dto.UserName);

      if (user == null)
      {
        return BadRequest();
      }

      int.TryParse(
          _configuration.GetSection("Auth:PwdSize").Value,
          out var size);

      if (size == 0)
      {
        throw new ArgumentException();
      }

      var pwd = PasswordService.HashPassword(dto.Password, user.Salt, size);

      if (user.Password != pwd)
      {
        return BadRequest();
      }

      var tokenHandler = new JwtSecurityTokenHandler();

      var key = Encoding.UTF8.GetBytes(_configuration["Auth:Key"]);

      var tokenDescription = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
          }),

        Expires = DateTime.Now.AddDays(1),
        SigningCredentials = new SigningCredentials(
              new SymmetricSecurityKey(key),
              SecurityAlgorithms.HmacSha256Signature)
      };

      var securityToken = tokenHandler.CreateToken(tokenDescription);

      var token = tokenHandler.WriteToken(securityToken);

      return Ok(new { user.Username, token });
    }

  }
}
