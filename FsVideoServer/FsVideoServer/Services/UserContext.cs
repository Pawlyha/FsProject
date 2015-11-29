using DbLibrary;
using System.Data.Entity;

namespace FsVideoServer.Models
{
    public class UserContext : DbContext
    {
        public UserContext() : base("ClientConnection") { }

        public virtual DbSet<UserSession> UserConnections { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // UserSession configuration
            var userSessionConfig = modelBuilder.Entity<UserSession>();
            userSessionConfig.HasKey(m => m.Id);
            userSessionConfig.Property(m => m.UserName).IsRequired();
            userSessionConfig.Property(m => m.Password).IsRequired();


            base.OnModelCreating(modelBuilder);
        }
    }


}