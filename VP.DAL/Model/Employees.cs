namespace VP.DAL.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employees
    {
        public int id { get; set; }

        [Required]
        [StringLength(150)]
        public string fullName { get; set; }

        public int departmentID { get; set; }

        [Required]
        [StringLength(200)]
        public string address { get; set; }

        public int countryID { get; set; }

        public int cityID { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        [Required]
        [StringLength(20)]
        public string phoneNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string login { get; set; }

        [Required]
        [StringLength(100)]
        public string password { get; set; }

        public int vacationStatus { get; set; }

        [Column(TypeName = "date")]
        public DateTime workbegin { get; set; }
    }
}
