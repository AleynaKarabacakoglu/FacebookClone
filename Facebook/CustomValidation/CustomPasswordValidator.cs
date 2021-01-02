using Common.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.CustomValidation
{
    public class CustomPasswordValidator : IPasswordValidator<UserDto>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<UserDto> manager, UserDto user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();
            if(password.ToLower().Contains(user.Name.ToLower()))
            {
                errors.Add(new IdentityError(){
                    Code = "PasswordContainsUserName",Description = "Şifre alanı kullanıcı adı içeremez."});
            }
            if (password.ToLower().Contains("1234"))
            {
                errors.Add(new IdentityError()
                {
                    Code = "PasswordContains1234",
                    Description = "Şifre alanı ardışık sayı içeremez."
                });
            }

            if(errors.Count==0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            if(errors.Count==0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
        }
    }
}
