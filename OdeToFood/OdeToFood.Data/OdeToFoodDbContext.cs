using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdeToFood.Data
{
    public class OdeToFoodDbContext: DbContext
    {
        public OdeToFoodDbContext(DbContextOptions<OdeToFoodDbContext> options): base(options)
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }

        public static implicit operator OdeToFoodDbContext(OdeToFoodContext v)
        {
            throw new NotImplementedException();
        }
    }
}
