﻿using Library.Data;
using Library.Model;
using Library.Model.DTO;
using Library.Repository.AccountReponsitory;
using Microsoft.AspNetCore.Mvc;



namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountReponsitory accountRepo;
       

        public AccountController(IAccountReponsitory repo)
        {
            accountRepo = repo;
           
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

