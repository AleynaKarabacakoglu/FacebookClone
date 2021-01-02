using Common.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.CustomValidation
{
    public class CustomUserValidator : IUserValidator<UserDto>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<UserDto> manager, UserDto user)
        {
            
            List<IdentityError> errors = new List<IdentityError>();
            string[] Digits = {" 0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            foreach (var item in Digits)
            {
                if(user.Name[0].ToString()==item)
                {
                    errors.Add(new IdentityError() { Code = "NameContainsFirstLetterDigitContains", Description = "Adınızın ilk karakteri sayısal değer olamaz." });
                }

            }
            if (errors.Count == 0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            if (errors.Count == 0)
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
