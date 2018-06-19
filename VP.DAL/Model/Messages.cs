namespace VP.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Messages
    {
        public int id { get; set; }

        public int fromID { get; set; }

        public int toID { get; set; }

        [Required]
        [StringLength(100)]
        public string title { get; set; }

        [Required]
        [StringLength(1000)]
        public string message { get; set; }

        public DateTime MessageTime { get; set; }
    }
}
