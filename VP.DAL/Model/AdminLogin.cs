namespace VP.DAL.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AdminLogin")]
    public partial class AdminLogin
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string login { get; set; }

        [Required]
        [StringLength(100)]
        public string password { get; set; }
    }
}
