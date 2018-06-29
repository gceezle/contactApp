﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using contactApp.Contracts;
using contactApp.Data;

namespace contactApp.Services
{
    public abstract class BaseRepository<T> : IBaseRepository<T>
        where T : class, new()
    {
        protected ContactAppDbContext _dbContext { get; set; }

        public async Task<T> GetAsync(int id)
        {
            return await _dbContext.FindAsync<T>(id);
        }

        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public async Task InsertAsync(T entity)
        { 
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
             _dbContext.Set<T>().Remove(entity);
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

       
    }
}