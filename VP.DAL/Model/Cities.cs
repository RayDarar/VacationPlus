namespace VP.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cities
    {
        public int id { get; set; }

        [Required]
        [StringLength(80)]
        public string name { get; set; }

        [StringLength(200)]
        public string description { get; set; }

        public int countryID { get; set; }
    }
}
