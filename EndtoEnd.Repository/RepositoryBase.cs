using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using EndtoEnd.Entity;

namespace EndtoEnd.Repository
{
    public abstract class RepositoryBase<TContext> : IDisposable
        where TContext : DbContext, IDisposedTracker, new()
    {
        private TContext _dataContext;

        protected virtual TContext DataContext
        {
            get
            {
                if (_dataContext == null || _dataContext.IsDisposed)
                {
                    _dataContext = new TContext();
                    //See http://msdn.microsoft.com/en-us/library/dd456853.aspx for details on this property and what it does
                    //Disable proxy creation to allow serialization and prevent 
                    //the "In order to serialize the parameter, add the type to the known types collection for the operation using ServiceKnownTypeAttribute" error
                    AllowSerialization = true;
                }
                return _dataContext;
            }
        }

        protected virtual bool AllowSerialization
        {
            get
            {
                return _dataContext.Configuration.ProxyCreationEnabled;
            }
            set
            {
                _dataContext.Configuration.ProxyCreationEnabled = !value;
            }
        }

        protected virtual T Get<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            if (predicate != null)
            {
                using (DataContext)
                {
                    return DataContext.Set<T>().Where(predicate).SingleOrDefault();
                }
            }
            else
            {
                throw new ApplicationException("Predicate value must be passed to Get<T>.");
            }
        }

        protected virtual IQueryable<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            try
            {
                var coll = DataContext.Set<T>();
                if (predicate != null)
                {
                    return coll.Where(predicate);
                }
                return coll;

            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }

        protected virtual IQueryable<T> GetList<T, TKey>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, TKey>> orderBy) where T : class
        {
            try
            {
                return GetList(predicate).OrderBy(orderBy);
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }

        protected virtual IQueryable<T> GetList<T, TKey>(Expression<Func<T, TKey>> orderBy) where T : class
        {
            try
            {
                return GetList<T>().OrderBy(orderBy);
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }

        protected virtual IQueryable<T> GetList<T>() where T : class
        {
            try
            {
                return DataContext.Set<T>();
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }

        protected OperationStatus ExecuteStoreCommand(string cmdText, params object[] parameters)
        {
            var opStatus = new OperationStatus { Status = true };

            try
            {
                opStatus.RecordsAffected = DataContext.Database.ExecuteSqlCommand(cmdText, parameters);
            }
            catch (Exception exp)
            {
                OperationStatus.CreateFromException("Error executing store command: ", exp);
            }
            return opStatus;
        }

        protected virtual OperationStatus Save<T>(T entity) where T : class
        {
            OperationStatus opStatus = new OperationStatus { Status = true };

            try
            {
                //Custom attaching/adding of entity could be done here
                opStatus.Status = DataContext.SaveChanges() > 0;
            }
            catch (Exception exp)
            {
                opStatus = OperationStatus.CreateFromException("Error saving " + typeof(T) + ".", exp);
            }

            return opStatus;
        }

        public OperationStatus Add<T>(T entity) where T : class
        {
            OperationStatus opStatus = new OperationStatus { Status = true };
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                DataContext.Set<T>().Add(entity);
                DataContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                opStatus = OperationStatus.CreateFromException("Error Adding " + typeof(T) + ".", dbEx);
            }
            return opStatus;
        }
        public OperationStatus Delete<T>(T entity) where T : class
        {
            OperationStatus opStatus = new OperationStatus { Status = true };
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                //DataContext.Entry(typeof(T)).State = EntityState.Deleted;
                DataContext.Set<T>().Remove(entity);
                DataContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                opStatus = OperationStatus.CreateFromException("Error deleting " + typeof(T) + ".", dbEx);
            }
            return opStatus;
        }

        public virtual void Dispose()
        {
            if (DataContext != null) 
                DataContext.Dispose();
        }
    }
}