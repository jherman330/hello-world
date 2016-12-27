using FirstEnergy.Logging;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EnterpriseRequests.Business
{
    //TODO: update these using statements with the fully qualified project namespace and move them out of the namespace declaration
    using Data;
    using Models;

    public interface IBaseBusiness<T> : IDisposable
    {
        /// <summary>
        /// Delete entity from database.
        /// </summary>
        /// <param name="id">Id of entity to delete.</param>
        /// <returns></returns>
        Task Delete(int id);

        /// <summary>
        /// Set the IsActive flage to false and save the entity.
        /// </summary>
        /// <param name="id">Id of entity to deactivate.</param>
        /// <returns></returns>
        Task Deactivate(int id);

        /// <summary>
        /// Return entity from context cache or database.
        /// </summary>
        /// <param name="id">Id of entity to be returned.</param>
        /// <returns>Entity</returns>
        Task<T> Find(int id);

        /// <summary>
        /// Return entity from context cache or database along with specified related properties.
        /// </summary>
        /// <param name="id">Id of entity to be returned.</param>
        /// <param name="includeProperties">Related properties to retrieve with entity.</param>
        /// <returns>Entity with related properties</returns>
        Task<T> FindIncluding(int id, params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Save new entity to database.
        /// </summary>
        /// <param name="item">Entity to be saved.</param>
        /// <returns>Saved entity</returns>
        Task<T> Add(T item);

        /// <summary>
        /// Mark specified entity as Modified and save changes.
        /// </summary>
        /// <param name="item">Entity to update.</param>
        /// <returns>Updated entity.</returns>
        Task<T> Update(T item);

        /// <summary>
        /// A queryable projection of entity that has AsNoTracking applied to it so EF will not load everything into the context. This is only meant to be used for the grids!
        /// </summary>
        /// <returns></returns>
        IQueryable<T> QueryableGridItems();
    }

    public abstract class BaseBusiness<T> : IBaseBusiness<T> where T : BaseModel, new()
    {
        protected readonly ProjectContext Ctx;
        protected readonly IEventLogger Log;
        protected readonly string UserId;

        protected BaseBusiness(string userSapId)
        {
            Ctx = new ProjectContext();
            UserId = userSapId;
            Log = EventLogger.GetLogger(GetType());
        }

        public virtual IQueryable<T> QueryableGridItems()
        {
            return Ctx.Set<T>().AsNoTracking();
        }

        public virtual async Task<T> FindIncluding(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Ctx.Set<T>().AsQueryable();
            if (includeProperties != null)
            {
                query = includeProperties.Aggregate(query, (current, exp) => current.Include(exp));
            }
            return await query.SingleAsync(o => o.Id == id);
        }

        public virtual async Task<T> Find(int id)
        {
            return await Ctx.Set<T>().FindAsync(id);
        }

        public virtual async Task Delete(int id)
        {
            T model = new T { Id = id };
            Ctx.Entry(model).State = EntityState.Deleted;
            await Ctx.SaveChangesAsync(UserId);
        }

        public virtual async Task Deactivate(int id)
        {
            T model = new T { Id = id, IsActive = false };
            Ctx.Entry(model).State = EntityState.Unchanged;
            Ctx.Entry(model).Property(x => x.IsActive).IsModified = true;
            //we are only updating the IsActive flag here, but validation may fail on other required properties we aren't filling in (ex: Name)
            Ctx.Configuration.ValidateOnSaveEnabled = false;
            await Ctx.SaveChangesAsync(UserId);
        }

        public virtual async Task<T> Add(T item)
        {
            Ctx.Entry(item).State = EntityState.Added;
            await Ctx.SaveChangesAsync(UserId);
            return item;
        }

        public virtual async Task<T> Update(T item)
        {
            Ctx.Entry(item).State = EntityState.Modified;
            await Ctx.SaveChangesAsync(UserId);
            return item;
        }

        public void Dispose()
        {
            Log.Dispose();
            if (Ctx != null)
            {
                Ctx.Dispose();
            }
        }
    }
}
