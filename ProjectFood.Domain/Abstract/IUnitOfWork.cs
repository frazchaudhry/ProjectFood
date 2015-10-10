using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ProjectFood.Domain.Concrete;
using ProjectFood.Domain.Entities;

namespace ProjectFood.Domain.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Recipe> RecipeRepository { get; }
        IGenericRepository<Region> RegionRepository { get; }
        IGenericRepository<Category> CategoryRepository { get; }
        void Save();
    }
}
