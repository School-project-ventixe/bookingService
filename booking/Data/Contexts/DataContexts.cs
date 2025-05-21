using booking.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace booking.Data.Contexts
{
    public class DataContexts(DbContextOptions<DataContexts> options) : DbContext(options)
    {
        public DbSet<BookingEntity> Bookings { get; set; }
    }
}
