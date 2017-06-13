using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Connected_Vehicles.Infrastructure.RestClients
{
    public class GlobalRestClient<T> : IGlobalRestClient<T> where T: class
    {
        private readonly RestClient _client;

        #region CTOR
        public GlobalRestClient(string Url)
        {
            if (!string.IsNullOrEmpty(Url))
            {
                _client = new RestClient(Url);

            }
        }
        #endregion

        #region Implement IGlobalRestClient
        /// <summary>
        /// Specify Method for Add APIs
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="APIMethodURL"></param>
        /// <returns></returns>
        public string Add(T obj, string APIMethodURL)
        {
            var request = new RestRequest(APIMethodURL, Method.POST) { RequestFormat = DataFormat.Json };

            request.JsonSerializer = new RestSharpDataContractJsonSerializer();

            request.AddBody(obj);

            var response = _client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return "OK";
            }
            else
            {
                return "Unable to Add Object Check the Server Log";
            }
        }
        

       /// <summary>
       /// Update API wrapper 
       /// </summary>
       /// <param name="obj"></param>
       /// <param name="APIMethodURL"></param>
       /// <returns></returns>
        public string Update(T obj, string APIMethodURL)
        {
            var request = new RestRequest(APIMethodURL, Method.PUT) { RequestFormat = DataFormat.Json };
            request.JsonSerializer = new RestSharpDataContractJsonSerializer();
            request.AddBody(obj);
            var response = _client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return "OK";
            }
            else
            {
                return "Unable to Update Object Check the Server Log";
            }
        }
       
       /// <summary>
       /// Delete API wrapper
       /// </summary>
       /// <param name="APIMethodURL"></param>
       /// <returns></returns>
        public string Delete(string APIMethodURL)
        {
            var request = new RestRequest(APIMethodURL, Method.DELETE) { RequestFormat = DataFormat.Json };

            var response = _client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                char[] charsToTrim = { '\"' };
                if (response.Content.Trim(charsToTrim) == "Conflict Exist")
                {
                    return "Conflict Exist";
                }
                return "OK";
            }
            else
            {
                return "Unable to Delete Object Check the Server Log";
            }
        }
       
        /// <summary>
        /// FindByID API wrapper
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="APIMethodURL"></param>
        /// <returns></returns>
        public T FindById(int Id, string APIMethodURL)
        {
            try
            {
                var request = new RestRequest(APIMethodURL + "?Id=" + Id, Method.GET) { RequestFormat = DataFormat.Json };
                var response = _client.Execute(request);
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Get All API wrapper
        /// </summary>
        /// <param name="APIMethodURL"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAll(string APIMethodURL)
        {
            try
            {
                var request = new RestRequest(APIMethodURL, Method.GET) { RequestFormat = DataFormat.Json };
                var response = _client.Execute(request);
                return JsonConvert.DeserializeObject<IEnumerable<T>>(response.Content);
            }
            catch (Exception)
            {

                return null;
            }


        }
       
        /// <summary>
        /// Get with filter Wrapper
        /// </summary>
        /// <param name="Parametrs"></param>
        /// <param name="APIMethodURL"></param>
        /// <returns></returns>
        public IEnumerable<T> GetWithFilter(Dictionary<String, object> Parametrs, string APIMethodURL)
        {

            try
            {
                var request = new RestRequest(APIMethodURL, Method.GET) { RequestFormat = DataFormat.Json };
                if (Parametrs.Count() > 0)
                {
                    foreach (var item in Parametrs)
                    {
                        request.AddParameter(item.Key, item.Value, ParameterType.QueryString);
                    }
                }
                var response = _client.Execute(request);
                return JsonConvert.DeserializeObject<IEnumerable<T>>(response.Content);
            }
            catch (Exception)
            {
                
                return null;
            }


        }
        #endregion 

    }
}

