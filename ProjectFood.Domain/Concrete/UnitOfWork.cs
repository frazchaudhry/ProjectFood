using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectFood.Domain.Abstract;
using ProjectFood.Domain.Entities;

namespace ProjectFood.Domain.Concrete
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private EfdbContext context = new EfdbContext();
        private bool disposed = false;

        private IGenericRepository<Recipe> recipeRepository;

        public IGenericRepository<Recipe> RecipeRepository
        {
            get
            {
                recipeRepository = new GenericRepository<Recipe>(context);
                return recipeRepository;
            }
        }

        private IGenericRepository<Region> regionRepository; 

        public IGenericRepository<Region> RegionRepository
        {
            get
            {
                return regionRepository;
            }
        }

        private IGenericRepository<Category> categoryRepository;

        public IGenericRepository<Category> CategoryRepository
        {
            get
            {
                return categoryRepository;
            }
        }

        public UnitOfWork(IGenericRepository<Recipe> recipeRepository, IGenericRepository<Region> regionRepository,
            IGenericRepository<Category> categoryRepository)
        {
            this.recipeRepository = recipeRepository;
            this.regionRepository = regionRepository;
            this.categoryRepository = categoryRepository;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
