using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarAPI.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
	    protected readonly DbContext Context;
		
		public Repository(DbContext context)
	    {
		    Context = context;
		}

		public void Add(TEntity entity)
		{
			Context.Add(entity);
		}

	    public void Update(TEntity entity)
	    {
		    Context.Update(entity);
	    }

		public void AddRange(IEnumerable<TEntity> entities)
		{
			Context.AddRange(entities);
		}

		public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
		{
			return Context.Set<TEntity>().Where(predicate);
		}

		public TEntity Get(Guid id)
	    {
			return Context.Set<TEntity>().Find(id);
	    }
	    public IEnumerable<TEntity> GetAll()
	    {
		    return Context.Set<TEntity>().ToList();
	    }

		public void Remove(TEntity entity)
		{
			Context.Remove(entity);
		}

		public void RemoveRange(IEnumerable<TEntity> entities)
		{
			Context.RemoveRange(entities);
		}
	}
}
