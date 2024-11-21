using Microsoft.EntityFrameworkCore;
using TanJingTing_Project.Models;

namespace TanJingTing_Project.Data
{
    public class MemberDbContext : DbContext
    {
        public MemberDbContext(DbContextOptions<MemberDbContext> options)
        : base(options)
        {
        }

        public DbSet<Booking>? Booking { get; set; }
    }
}
