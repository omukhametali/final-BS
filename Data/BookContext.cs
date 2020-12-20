using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using final_BS.Data.Models;

namespace final_BS.Models
{
    public class BookContext: DbContext
    {
        public DbSet<Book> Books { set; get; }
        public DbSet<Purchase> Purchases { set; get; }
        public DbSet<User> Users { set; get; }
        public DbSet<Review> Reviews { set; get; }
    }

    
}
