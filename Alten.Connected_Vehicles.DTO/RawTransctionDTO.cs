using Alten.Connected_vehicle.Model;
using System;
using System.Collections.Generic;


namespace Alten.Connected_Vehicles.DTO
{
    // <summary>
    /// Data transfer Objects, used for simplifying DAL obejcts to the client to deliver only the data scope needed for the client 
    /// Exposing DAL objects directly to is not recommended 
    /// </summary>
    public class RawTransctionDTO
    {
        #region Properties 

        public int ID { get; set; }
        public byte[] RawData{ get; set; }
        public DateTime EntryDate { get; set; }
       
        #endregion

        #region CTOR
        public RawTransctionDTO(RawTransction RawTransDAL)
        {
            this.ID = RawTransDAL.ID;
            this.RawData = RawTransDAL.RawData;
            this.EntryDate = RawTransDAL.EntryDate.HasValue? RawTransDAL.EntryDate.Value : DateTime.MinValue;
            
        }
        #endregion


        #region Map Helpers 
        /// <summary>
        /// Map Collection of DAL objects Into List of DTOs
        /// </summary>
        /// <param name="Collection"></param>
        /// <returns></returns>
        public static IEnumerable<RawTransctionDTO> GetList(ICollection<RawTransction> Collection)
        {
            List<RawTransctionDTO> list = new List<RawTransctionDTO>();
            foreach (RawTransction Trans in Collection)
            {
                list.Add(new RawTransctionDTO(Trans));
            }
            return list;
        }
        /// <summary>
        /// Map DTO to DAL Object
        /// </summary>
        /// <returns></returns>
        public RawTransction GetDALObj()
        {
            return new RawTransction()
            {
                RawData = this.RawData,
                EntryDate = this.EntryDate
               
            };

        }
        #endregion 
    }
}
