using Microsoft.EntityFrameworkCore;
using CarAPI.Models;

namespace CarAPI.DAL
{
    public class CarApiContext : DbContext
    {
        public CarApiContext(DbContextOptions options)
            : base(options)
        {
        }

	    public DbSet<Car> Cars { get; set; }

	    public DbSet<Company> Companies { get; set; }
	}
}