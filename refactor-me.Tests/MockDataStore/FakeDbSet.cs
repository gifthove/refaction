namespace refactor_me.Tests.MockDataStore
{
    using refactor_me.data;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// Class FakeDbSet.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Data.Entity.IDbSet{T}" />
    public class FakeDbSet<T> : IDbSet<T>
     where T : class
    {
        /// <summary>
        /// The data
        /// </summary>
        ObservableCollection<T> _data;
        /// <summary>
        /// The query
        /// </summary>
        IQueryable _query;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeDbSet{T}"/> class.
        /// </summary>
        public FakeDbSet()
        {
            _data = new ObservableCollection<T>();
            _query = _data.AsQueryable();
        }

        /// <summary>
        /// Finds an entity with the given primary key values.
        /// If an entity with the given primary key values exists in the context, then it is
        /// returned immediately without making a request to the store.  Otherwise, a request
        /// is made to the store for an entity with the given primary key values and this entity,
        /// if found, is attached to the context and returned.  If no entity is found in the
        /// context or the store, then null is returned.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <returns>The entity found, or null.</returns>
        /// <exception cref="System.NotImplementedException">Derive from FakeDbSet<T> and override Find</exception>
        /// <remarks>The ordering of composite key values is as defined in the EDM, which is in turn as defined in
        /// the designer, by the Code First fluent API, or by the DataMember attribute.</remarks>
        public virtual T Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>T.</returns>
        public T Add(T item)
        {
            _data.Add(item);
            return item;
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>T.</returns>
        public T Remove(T item)
        {
            _data.Remove(item);
            return item;
        }

        /// <summary>
        /// Attaches the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>T.</returns>
        public T Attach(T item)
        {
            _data.Add(item);
            return item;
        }

        /// <summary>
        /// Detaches the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>T.</returns>
        public T Detach(T item)
        {
            _data.Remove(item);
            return item;
        }

        /// <summary>
        /// Creates a new instance of an entity for the type of this set.
        /// Note that this instance is NOT added or attached to the set.
        /// The instance returned will be a proxy if the underlying context is configured to create
        /// proxies and the entity type meets the requirements for creating a proxy.
        /// </summary>
        /// <returns>The entity instance, which may be a proxy.</returns>
        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        /// <summary>
        /// Creates a new instance of an entity for the type of this set or for a type derived
        /// from the type of this set.
        /// Note that this instance is NOT added or attached to the set.
        /// The instance returned will be a proxy if the underlying context is configured to create
        /// proxies and the entity type meets the requirements for creating a proxy.
        /// </summary>
        /// <typeparam name="TDerivedEntity">The type of entity to create.</typeparam>
        /// <returns>The entity instance, which may be a proxy.</returns>
        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> that represents a local view of all Added, Unchanged,
        /// and Modified entities in this set.  This local view will stay in sync as entities are added or
        /// removed from the context.  Likewise, entities added to or removed from the local view will automatically
        /// be added to or removed from the context.
        /// </summary>
        /// <value>The local view.</value>
        /// <remarks>This property can be used for data binding by populating the set with data, for example by using the Load
        /// extension method, and then binding to the local data through this property.  For WPF bind to this property
        /// directly.  For Windows Forms bind to the result of calling ToBindingList on this property</remarks>
        public ObservableCollection<T> Local
        {
            get { return _data; }
        }

        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable" /> is executed.
        /// </summary>
        /// <value>The type of the element.</value>
        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        /// <summary>
        /// Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable" />.
        /// </summary>
        /// <value>The expression.</value>
        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        /// <value>The provider.</value>
        IQueryProvider IQueryable.Provider
        {
            get { return _query.Provider; }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }  
    
}
