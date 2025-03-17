namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IGetByIdRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The entity if found, null otherwise.</returns>
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }

    public interface IGetAllRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of entities.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    }

    public interface IGetQueryable<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
    }

    public interface ICreateRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Creates a new entity in the database.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created entity.</returns>
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }

    public interface IUpdateRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated entity.</returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }

    public interface IDeleteRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Deletes an entity from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the entity was deleted, false if not found.</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}