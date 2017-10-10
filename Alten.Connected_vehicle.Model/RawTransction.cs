namespace Alten.Connected_vehicle.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RawTransction
    {
        public int ID { get; set; }

        public byte[] RawData { get; set; }

        public DateTime? EntryDate { get; set; }
    }
}
