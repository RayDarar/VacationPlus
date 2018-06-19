namespace VP.DAL.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class VPDB : DbContext
    {
        public VPDB()
            : base("name=VPDB")
        {
        }

        public virtual DbSet<AdminLogin> AdminLogin { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<FiredEmployees> FiredEmployees { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Requests> Requests { get; set; }
        public virtual DbSet<Vacations> Vacations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminLogin>()
                .Property(e => e.login)
                .IsUnicode(false);

            modelBuilder.Entity<AdminLogin>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<Cities>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Cities>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Countries>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Countries>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Departments>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Departments>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.fullName)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.phoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.login)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<FiredEmployees>()
                .Property(e => e.login)
                .IsUnicode(false);

            modelBuilder.Entity<FiredEmployees>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<FiredEmployees>()
                .Property(e => e.AdminMessage)
                .IsUnicode(false);

            modelBuilder.Entity<Messages>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Messages>()
                .Property(e => e.message)
                .IsUnicode(false);

            modelBuilder.Entity<Requests>()
                .Property(e => e.message)
                .IsUnicode(false);
        }
    }
}