namespace VP.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Requests
    {
        public int id { get; set; }

        public int fromID { get; set; }

        [Required]
        [StringLength(500)]
        public string message { get; set; }

        public int status { get; set; }

        public int type { get; set; }
    }
}
