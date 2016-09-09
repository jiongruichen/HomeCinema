using HomeCinema.Data.Infrastructure;
using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Services
{
    public static class UserExtensions
    {
        public static User GetSingleByUsername(this IEntityBaseRepository<User> userRepository, string username)
        {
            return userRepository.GetAll().FirstOrDefault(x => x.Username == username);
        }
    }
}