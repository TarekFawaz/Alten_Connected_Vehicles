using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alten.Connected_Vehicles.Infrastructure.Repository;
using Alten.Connected_Vehicles.Infrastructure.UnitOfWork;
using System.Data.Entity;

namespace Alten.Connected_Vehicles.MSSQLRepository
{
    public partial class UnitOfWork<T> : IUnitOfWork<T> , IDisposable
        where T : class
    {
        #region Data Members 
        /// <summary>
        /// Generic repository 
        /// </summary>
        private IRepository<T> _Repo;
        /// <summary>
        /// DBContext for operations
        /// </summary>
        private DbContext _DbContext;
        /// <summary>
        ///  indicator if Object disposed or not
        /// </summary>
        private bool disposed = false;
        #endregion 


        public IRepository<T> Repo
        {
            get
            {

                if (_Repo == null)
                    _Repo = new MSSQLRepository<T>(_DbContext);
                return _Repo;
            }
        }


        public UnitOfWork(DbContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext is null"); /// if Database context not initalized Through Exception
            this._DbContext = dbContext;
        }

        public void Commit()
        {
            _DbContext.SaveChanges();
        }

        #region Disposing 
        protected virtual void Dispose(bool disposing)
        {

            if (!this.disposed)
            {
                this.Commit(); // Commiting the changes if any to the database before disposing and finalize the UoW object.
                             
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion 

    }
}
