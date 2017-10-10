namespace Alten.Connected_vehicle.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transaction
    {
        [Required]
        [StringLength(6)]
        public string RegNo { get; set; }

        public DateTime EntryDate { get; set; }

        public bool Status { get; set; }

        public int ID { get; set; }
    }
}
