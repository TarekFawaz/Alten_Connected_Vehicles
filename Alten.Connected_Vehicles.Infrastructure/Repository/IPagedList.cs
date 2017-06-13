using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Connected_Vehicles.Infrastructure.Repository
{
    /// <summary>
    /// Basic Implementation for Pagination 
    /// </summary>
    /// <typeparam name="T">Domain Model to be paginated</typeparam>
    internal interface IPagedList<T> : IList<T> 
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }


    }
}
