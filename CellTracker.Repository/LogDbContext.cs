using System.Threading.Tasks;

using CellTracker.Repository.Entities;

using Microsoft.EntityFrameworkCore;

namespace CellTracker.Repository
{
    public delegate void MigratedCallback(LogDbContext context);

    public class LogDbContext : DbContext, IDbContext
    {

        public static bool Migrated;

        public LogDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public LogDbContext MigrateOnce(MigratedCallback callback = null)
        {

            lock (Database)
            {
                if (!Migrated)
                {
                    Database.Migrate();
                    Migrated = true;

                    callback?.Invoke(this);
                }
            }

            return this;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LogRecord>()
                .HasDiscriminator<string>(nameof(LogRecord.CellActionTypeString))
                .HasValue<SmsRecord>(CellActionType.Sms.ToString())
                .HasValue<CallRecord>(CellActionType.Call.ToString());
        }

        public DbSet<T> GetDbSet<T>()
            where T : class
        {
            return Set<T>();
        }

        public int Save()
        {
            return SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return SaveChangesAsync();
        }
    }
}