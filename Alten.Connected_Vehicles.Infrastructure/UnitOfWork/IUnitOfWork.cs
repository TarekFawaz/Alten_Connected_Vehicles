using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alten.Connected_Vehicles.Infrastructure.Repository;

namespace Alten.Connected_Vehicles.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork<T> : IDisposable
        where T : class
    {
        IRepository<T> Repo { get; }
        void Commit();
    }
}
