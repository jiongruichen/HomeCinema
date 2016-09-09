using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Data.Configurations
{
    public class RoleConfiguration : EntityBaseConfiguration<Role>
    {
        public RoleConfiguration()
        {
            Property(ur => ur.Name).IsRequired().HasMaxLength(50);
        }
    }
}