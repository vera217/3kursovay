using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using model;

namespace logic.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Items> items { get; set; }
        public DbSet<AppraisalItems> appraisal_items { get; set; }
        public DbSet<Client> clients { get; set; }

    }
}
