using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UseCases.RepositoryContract.Abstraction;

namespace DataAccess.Repository.Abstraction
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ICinemaContext _context;
        private readonly DbSet<TEntity> _entities;
        private readonly Type _type;
        public BaseRepository(ICinemaContext context)
        {
            _context = context;
            _type = typeof(TEntity);
            _entities = _context.Set<TEntity>(_type);
        }

        public virtual void Add(TEntity entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public virtual void Add(List<TEntity> entities)
        {
            _entities.AddRange(entities);
            _context.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(List<TEntity> entities)
        {
            _entities.RemoveRange(entities);
            _context.SaveChanges();
        }

        public virtual bool DoesExist(Expression<Func<TEntity, bool>> filter)
        => _entities.Any(filter);

        public virtual TEntity Find(int id)
        => _entities.Find(id);

        public virtual List<TEntity> GetAll()
        => _entities.AsEnumerable().ToList();

        public virtual void Update(TEntity entity)
        {
            _entities.Update(entity);
            _context.SaveChanges();
        }

        public virtual void Update(List<TEntity> entities)
        {
            _entities.UpdateRange(entities);
            _context.SaveChanges();
        }
    }
}
