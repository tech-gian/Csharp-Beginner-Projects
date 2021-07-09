using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Cars
{
    public class CarDb: DbContext
    {
        public DbSet<Car> Cars { get; set; }

    }
}
