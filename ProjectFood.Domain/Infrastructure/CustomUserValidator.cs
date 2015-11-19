using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using ProjectFood.Domain.Entities;

namespace ProjectFood.Domain.Infrastructure
{
    class CustomUserValidator : UserValidator<User, int>
    {
        public CustomUserValidator(AppUserManager mgr) : base(mgr) { }

        public override async Task<IdentityResult> ValidateAsync(User user)
        {
            IdentityResult result = await base.ValidateAsync(user);

            //if (!user.Email.ToLower().EndsWith("@example.com"))
            //{
            //    var errors = result.Errors.ToList();
            //    errors.Add("Only example email addresses are allowed");
            //    result = new IdentityResult(errors);
            //}
            return result;
        }
    }
}
