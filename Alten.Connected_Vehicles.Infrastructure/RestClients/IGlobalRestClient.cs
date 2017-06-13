using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Connected_Vehicles.Infrastructure.RestClients
{
    public interface IGlobalRestClient <T> where T: class
    {
        string Add(T obj, string APIMethodURL);
        string Update(T obj, string APIMethodURL);
        string Delete(string APIMethodURL);
        T FindById(int Id, string APIMethodURL);
        IEnumerable<T> GetAll(string APIMethodURL);
        IEnumerable<T> GetWithFilter(Dictionary<String, object> Parametrs, string APIMethodURL);
        
    }
}
