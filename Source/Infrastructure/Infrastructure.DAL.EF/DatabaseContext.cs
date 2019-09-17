using Core.Model;
using Infrastructure.DAL.EF.Seed;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace Infrastructure.DAL.EF
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Site> Sites { get; set; }

        public IQueryable<Site> SitesQueryable
        {
            get => Sites;
            set => Sites = (DbSet<Site>)value;
        }

        public DbSet<Participation> Participations { get; set; }

        public IQueryable<Participation> ParticipationsQueryable
        {
            get => Participations.Include(p => p.Site);
            set => Participations = (DbSet<Participation>)value;
        }

        public DbSet<Participant> Participants { get; set; }

        public IQueryable<Participant> ParticipantsQueryable
        {
            get => Participants.Include(p => p.Participation);
            set => Participants = (DbSet<Participant>)value;
        }

        public DbSet<FailedTransaction> FailedTransactions { get; set; }

        public IQueryable<FailedTransaction> FailedTransactionsQueryable
        {
            get => FailedTransactions.Include(ft => ft.Participation);
            set => FailedTransactions = (DbSet<FailedTransaction>)value;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            #region Data Seed

            modelBuilder.Seed();

            #endregion
        }
    }
}
