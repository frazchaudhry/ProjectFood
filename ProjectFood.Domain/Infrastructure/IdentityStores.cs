using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectFood.Domain.Concrete;
using ProjectFood.Domain.Entities;

namespace ProjectFood.Domain.Infrastructure
{
    public class RecipeUserStore : UserStore<User, Role, int,
        UserLogin, UserRole, UserClaim>
    {
        public RecipeUserStore(EfdbContext context)
            : base(context)
        {
        }
    }

    public class RecipeRoleStore : RoleStore<Role, int, UserRole>
    {
        public RecipeRoleStore(EfdbContext context)
            : base(context)
        {
        }
    } 
}
