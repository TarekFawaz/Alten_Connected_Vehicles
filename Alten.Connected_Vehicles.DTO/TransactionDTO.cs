using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alten.Connected_Vehicles.DAL;
namespace Alten.Connected_Vehicles.DTO
{

    /// <summary>
    /// Data transfer Objects, used for simplifying DAL obejcts to the client to deliver only the data scope needed for the client 
    /// Exposing DAL objects directly to is not recommended 
    /// </summary>
    public class TransactionDTO
    {
        #region Properties 

        public Guid ID { get; set; }
        public string RegNo { get; set; }
        public DateTime EntryDate { get; set; }
        public bool Status { get; set; }
        #endregion

        #region CTOR
        public TransactionDTO(Transaction TransDAL)
        {
            this.ID = TransDAL.ID;
            this.RegNo = TransDAL.RegNo;
            this.EntryDate = TransDAL.EntryDate;
            this.Status = TransDAL.Status;
        }
        #endregion


        #region Map Helpers 
        /// <summary>
        /// Map Collection of DAL objects Into List of DTOs
        /// </summary>
        /// <param name="Collection"></param>
        /// <returns></returns>
        public static IEnumerable<TransactionDTO> GetList(ICollection<Transaction> Collection)
        {
            List<TransactionDTO> list = new List<TransactionDTO>();
            foreach (Transaction Trans in Collection)
            {
                list.Add(new TransactionDTO(Trans));
            }
            return list;
        }
        /// <summary>
        /// Map DTO to DAL Object
        /// </summary>
        /// <returns></returns>
        public Transaction GetDALObj()
        {
            return new Transaction()
            {
                RegNo = this.RegNo,
                EntryDate = this.EntryDate,
                Status = this.Status,
                ID = this.ID != null ? this.ID : Guid.NewGuid()
            };
            
        }
        #endregion 
    }
}
