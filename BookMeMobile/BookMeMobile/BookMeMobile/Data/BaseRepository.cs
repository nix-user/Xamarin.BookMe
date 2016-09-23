using System.Collections.Generic;
using System.Threading.Tasks;
using BookMeMobile.Data.Abstract;
using BookMeMobile.OperationResults;

namespace BookMeMobile.Data
{
    /// <summary>
    /// Contains shared logic and elements for repositories
    /// </summary>
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
    {
        protected readonly HttpService HttpService = new HttpService();

        public abstract Task<BaseOperationResult<IEnumerable<TEntity>>> GetAll();

        public abstract Task<BaseOperationResult<TEntity>> GetById(int id);

        public abstract Task<BaseOperationResult> Add(TEntity reservation);

        public abstract Task<BaseOperationResult> Remove(int id);
    }
}