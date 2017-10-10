namespace Alten.Connected_vehicle.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vehicle
    {
        [Required]
        [StringLength(25)]
        public string ID { get; set; }

        [Key]
        [StringLength(10)]
        public string RegNo { get; set; }

        public Guid? CustomerID { get; set; }

        public DateTime? LastUpdateTime { get; set; }

        public bool? ActiveStatus { get; set; }

        public DateTime? CreatedOn { get; set; }

        public bool? Active { get; set; }

        public bool? Deleted { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
