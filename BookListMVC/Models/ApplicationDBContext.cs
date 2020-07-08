using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Models
{
    public class ApplicationDBContext : DbContext //bridge between the enity and database
    {
        // created this constructor
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }

        internal Task FindAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
    
}
