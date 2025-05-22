using System.Data.Entity;

namespace WpfAppNeeeeeee.Models
{
    public class SchoolAppContext : DbContext
    {
        static SchoolAppContext()
        {
            Database.SetInitializer<SchoolAppContext>(null);
        }

        public SchoolAppContext() : base("name=SchoolAppContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
} 