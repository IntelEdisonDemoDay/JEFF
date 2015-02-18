using JEFF.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;
using System.Linq;

namespace JEFF.DataAccess
{
    /// <summary>
    /// MongoBaseRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MongoBaseRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoBaseRepository{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public MongoBaseRepository(MongoCollection<T> collection)
        {
            Collection = collection;
        }

        /// <summary>
        /// Gets or sets the collection.
        /// </summary>
        /// <value>
        /// The collection.
        /// </value>
        public MongoCollection<T> Collection { get; private set; }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            return Collection.FindAll().AsEnumerable();
        }

        /// <summary>
        /// Gets an item by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual T GetById(string id)
        {
            var query = Query<T>.EQ(m => m.Id, new ObjectId(id));
            return Collection.FindOne(query);
        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public virtual void Update(T item)
        {
            Collection.Save(item);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void Delete(string id)
        {
            var query = Query<T>.EQ(m => m.Id, new ObjectId(id));
            var result = Collection.Remove(query);
        }
    }
}
