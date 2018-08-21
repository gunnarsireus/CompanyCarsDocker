using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace CarApi.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
	    TEntity Get(Guid id);
	    IEnumerable<TEntity> GetAll();
	    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
	    void Add(TEntity entity);

	    void Update(TEntity entity);
		void AddRange(IEnumerable<TEntity> entities);
	    void Remove(TEntity entity);
	    void RemoveRange(IEnumerable<TEntity> entities);
	}
}
