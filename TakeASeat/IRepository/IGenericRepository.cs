﻿using System.Linq.Expressions;
using X.PagedList;
using TakeASeat.RequestUtils;

namespace TakeASeat.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAll(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null           
            );
        Task<IPagedList<T>> PaginatedGetAll(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null,
            RequestParams requestParams= null
            );
        Task<T> Get(
            Expression<Func<T, bool>> expression = null,
            List<string> includes = null
            );
        Task Create(T entity);   
        Task CreateRange(IEnumerable<T> entities);
        Task Delete(int Id);
        void Update(T entity);

    }
}
