﻿using Library.Data;

using Library.Server;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolLibrary.DTO;
using SchoolLibrary.Model;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountReponsitory accountRepo;
        private readonly MyDB context;

        public AccountController(IAccountReponsitory repo,MyDB context)
        {
            accountRepo = repo;
           
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignInModel signUpModel)
        {
            try
            {
                var result = await accountRepo.SignUpAsync(signUpModel);
                if (result.Succeeded)
                {
                    return Ok(result.Succeeded);
                }
                return BadRequest();
                
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            try
            {
                var result = await accountRepo.SignInAsync(signInModel);

                if (string.IsNullOrEmpty(result))
                {
                    return BadRequest(BaseReponsitory<string>.WithMessage("Mật khẩu hoặc tên đăng nhập sai", 400));
                }

                return Ok(BaseReponsitory<string>.WithData(result,200));
            }
            catch
            {
                return BadRequest();
            }
        }
       
    }
}
