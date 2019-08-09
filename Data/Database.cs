using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;

namespace Data
{
    public class Database<ApplicationDbContext> : IDatabaseInitializer<ApplicationDbContext> where ApplicationDbContext : DbContext
    {
        public Database()
        {

        }


        public void InitializeDatabase(ApplicationDbContext context)
        {
            throw new NotImplementedException();
        }

        
    }
}
