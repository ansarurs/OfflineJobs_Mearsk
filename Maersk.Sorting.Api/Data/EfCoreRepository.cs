using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maersk.Sorting.Api.Data
{
    public  abstract class EfCoreRepository<TEntity> : IRepository<TEntity> where TEntity : class 
       
        //where TContext : DbContext
    {
        private readonly SortJobDbContext context;
        private readonly DbSet<TEntity> entities;
        public EfCoreRepository(SortJobDbContext context)
        {
            this.context = context;
            entities = context.Set<TEntity>();
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

      

        public async Task<TEntity> Delete(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> Get(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Get(Guid jobId)
        {
            return await context.Set<TEntity>().FindAsync(jobId);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            
            await context.SaveChangesAsync();
            return entity;
        }

    }
}
