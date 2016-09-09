using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Data.Configurations
{
    public class GenreConfiguration : EntityBaseConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            Property(g => g.Name).IsRequired().HasMaxLength(50);
        }
    }
}