using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ProjectFood.Domain.Concrete;
using ProjectFood.Domain.Entities;

namespace ProjectFood.Domain.Infrastructure
{
    public class AppUserManager : UserManager<User>
    {
        public AppUserManager(IUserStore<User> store) : base(store)
        {   
        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            EfdbContext db = context.Get<EfdbContext>();
            AppUserManager manager = new AppUserManager(new UserStore<User>(db));

            return manager;
        }
    }
}
