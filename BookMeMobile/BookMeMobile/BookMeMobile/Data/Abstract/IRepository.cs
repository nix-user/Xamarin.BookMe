using System.Collections.Generic;
using System.Threading.Tasks;
using BookMeMobile.OperationResults;

namespace BookMeMobile.Data.Abstract
{
    /// <summary>
    /// Defines methods for working with data source
    /// </summary>
    /// <typeparam name="TEntity">The type of objects for working</typeparam>
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Method for getting all elements of specified type in data source
        /// </summary>
        /// <returns>operation result which will contain operation status code and collection of entities</returns>
        Task<BaseOperationResult<IEnumerable<TEntity>>> GetAll();

        /// <summary>
        /// Method for getting element with id equals to <paramref name="id"/>
        /// </summary>
        /// <returns>operation result which will contain operation status coed and collection of entities</returns>
        Task<BaseOperationResult<TEntity>> GetById(int id);

        /// <summary>
        /// Method for adding new element to data source
        /// </summary>
        /// <param name="reservation">Element to add</param>
        /// <returns>operation result</returns>
        Task<BaseOperationResult> Add(TEntity reservation);

        /// <summary>
        /// Method for remove element by <paramref name="id"/> from data source
        /// </summary>
        /// <param name="id">identifier of element to remove</param>
        /// <returns>operation result</returns>
        Task<BaseOperationResult> Remove(int id);
    }
}