namespace ErrorLoggerModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1() : base("Model1")
        {
            Database.SetInitializer<Model1>(new ErrorLogdbInitializer());
            //Database.SetInitializer<Model1>(null);
        }

        public DbSet<Errors> Errors { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<LogStatus> LogStatus { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
