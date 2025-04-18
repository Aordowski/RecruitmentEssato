using Microsoft.EntityFrameworkCore;

namespace ZadanieRekrutacyjne.DataFetch
{
    class WeatherDbContext : DbContext
    {
        private string connectionString;
        public DbSet<WeatherRecord> WeatherRecords { get; set; }

        public WeatherDbContext(string connectionStringInput)
        {
            connectionString = connectionStringInput;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(connectionString);
    }
}
